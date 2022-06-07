
using App.Application.Student.Relocation.Models;
using App.Application.Student.Relocation.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Relocation.Commands
{
   

    public class StudentRellocationCommand : IRequest<List<SearchRellocationModel>>
    {

        public long Id { get; set; }
        public int? ProfileId { get; set; }
        public short? SchoolTypeId { get; set; }
        public short? OldSchoolId { get; set; }
        public short? NewSchoolId { get; set; }
        public int? OldAssasNumber { get; set; }
        public int? NewAssasNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? SchoolLocationId { get; set; }
        public int? District { get; set; }
       

    }

    public class StudentRellocationCommandHandler : IRequestHandler<StudentRellocationCommand, List<SearchRellocationModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public StudentRellocationCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<SearchRellocationModel>> Handle(StudentRellocationCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<SearchRellocationModel> result = new List<SearchRellocationModel>();
            var StudentRellocation = request.Id != 0 ? context.Rellocations.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Rellocation();
            int CurrentUserId = await currentUser.GetUserId();

            StudentRellocation.Id = request.Id;
            StudentRellocation.ProfileId = request.ProfileId;
            StudentRellocation.NewSchoolId = request.NewSchoolId;
            StudentRellocation.OldSchoolId = request.OldSchoolId;
            StudentRellocation.NewAssasNumber = request.NewAssasNumber;
            StudentRellocation.OldAssasNumber = request.OldAssasNumber;
            StudentRellocation.SchoolLocationId = request.SchoolLocationId;
            StudentRellocation.District = request.District;
            StudentRellocation.SchoolTypeId = request.SchoolTypeId;


            if (request.Id != 0)
            {
                StudentRellocation.ModifiedOn = DateTime.Now; ;
                StudentRellocation.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                StudentRellocation.CreatedBy = CurrentUserId;
                StudentRellocation.CreatedOn = DateTime.Now;
                context.Rellocations.Add(StudentRellocation);
            }

            await context.SaveChangesAsync();

            result = await mediator.Send(new StudentRellocationSearchQuries() { Id = StudentRellocation.Id });

            return result.ToList();

        }
    }
}
