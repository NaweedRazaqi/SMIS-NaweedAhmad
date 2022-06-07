
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Commands
{
   
    public class CreateStudentRelative : IRequest<List<StudentRelativeModel>>
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public int? RelativeTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

    }

    public class CreateStudentRelativeHandler : IRequestHandler<CreateStudentRelative, List<StudentRelativeModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public CreateStudentRelativeHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
      

        public async Task<List<StudentRelativeModel>> Handle(CreateStudentRelative request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentRelativeModel> result = new List<StudentRelativeModel>();
            var StudentRelative = request.Id!=0 ? context.Relatives.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Relatives();
            int CurrentUserId = await currentUser.GetUserId();

            StudentRelative.Id = request.Id;
            StudentRelative.ProfileId = request.ProfileId;
           // StudentRelative.Name = request.Name;
            StudentRelative.RelativeTypeId = request.RelativeTypeId;

            if (request.Id!=0)
            {
                StudentRelative.ModifiedOn = DateTime.Now; ;
                StudentRelative.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                StudentRelative.CreatedBy = CurrentUserId;
                StudentRelative.CreatedOn = DateTime.Now;
                context.Relatives.Add(StudentRelative);
            }

            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentRelativeQuery() { Id = StudentRelative.Id});

            return result.ToList();

        }
    }

}
