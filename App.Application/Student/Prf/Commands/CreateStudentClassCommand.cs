
using App.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using Clean.Persistence.Services;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;

namespace App.Application.Student.Prf.Commands
{
    public class CreateStudentClassCommand : IRequest<List<SearchStudentClassModel>>
    {

        public decimal? Id { get; set; }
        public int? ProfileId { get; set; }
        public short? ClassTypeID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public short? ClassManagementId { get; set; }


    }

    public class CreateStudentClassCommandHandler : IRequestHandler<CreateStudentClassCommand, List<SearchStudentClassModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateStudentClassCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<SearchStudentClassModel>> Handle(CreateStudentClassCommand request, CancellationToken cancellationToken)
        { 
            IEnumerable<SearchStudentClassModel> result = new List<SearchStudentClassModel>();
            var studentclass = request.Id.HasValue ? context.StudentClasses.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.StudentClass();
            int CurrentUserId = await currentUser.GetUserId();

            studentclass.ProfileId = request.ProfileId;
            studentclass.ClassTypeId = request.ClassTypeID;
            studentclass.ClassManagementId = (short)request.ClassManagementId;


            if (request.Id.HasValue)
            {
                studentclass.ModifiedOn = DateTime.Now; ;
                studentclass.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                studentclass.CreatedBy = CurrentUserId;
                studentclass.CreatedOn = DateTime.Now;
                context.StudentClasses.Add(studentclass);
            }
            

            await context.SaveChangesAsync();

            result = await mediator.Send(new SearchStudentClassQuery() { ID = studentclass.Id });

            return result.ToList();
        }
    }

}