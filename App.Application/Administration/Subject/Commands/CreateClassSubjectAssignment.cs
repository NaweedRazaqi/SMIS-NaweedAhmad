using App.Application.Administration.Subject.Models;
using App.Application.Administration.Subject.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Administration.Subject.Commands
{
    public class CreateClassSubjectAssignment : IRequest<List<SearchSubjectAssignmentModel>>
    {

        public long? Id { get; set; }
        public short? SubjectId { get; set; }
        //public long? ClassManagementId { get; set; }
        public short? ClassTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public short? SchoolTypeId { get; set; }
    }

    public class CreateClassSubjectAssignmentHandler : IRequestHandler<CreateClassSubjectAssignment, List<SearchSubjectAssignmentModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateClassSubjectAssignmentHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }

        public async Task<List<SearchSubjectAssignmentModel>> Handle(CreateClassSubjectAssignment request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await currentUser.GetUserId();
            var SubjectAssign = request.Id.HasValue ? context.ClassSubjectManagements.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.ClassSubjectManagement();

            IEnumerable<SearchSubjectAssignmentModel> result = new List<SearchSubjectAssignmentModel>();

                 //SubjectAssign.Id = (long)request.Id;
                SubjectAssign.SubjectId = request.SubjectId;
                SubjectAssign.ClassTypeId = request.ClassTypeId;
                SubjectAssign.SchoolTypeId = request.SchoolTypeId;
        


            if (!request.Id.HasValue)
            {
                SubjectAssign.CreatedBy = (short)CurrentUserId;
                SubjectAssign.CreatedOn = DateTime.Now;
                context.ClassSubjectManagements.Add(SubjectAssign);
            }
            else
            {
                SubjectAssign.ModifiedBy += "," + CurrentUserId; ;
                SubjectAssign.ModifiedOn = DateTime.Now;
            }
            await context.SaveChangesAsync();
            result = await mediator.Send(new SearchSubjectAssigmentQueries() { Id = SubjectAssign.Id });
            return result.ToList();
        }
    }
}
