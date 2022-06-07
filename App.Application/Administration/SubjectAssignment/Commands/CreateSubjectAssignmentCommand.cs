using App.Application.SubjectAssignment.Models;
using App.Application.SubjectAssignment.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.SubjectAssignment.Commands
{
    public class CreateSubjectAssignmentCommand : IRequest<List<SearchSubjectAssignmentModel>>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int YearId { get; set; }
        public short SubjectManagementId { get; set; }
        public short ClassTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public short SchoolTypeId { get; set; }
        
    }
    public class CreateSubjectAssignmentCommandHandler : IRequestHandler<CreateSubjectAssignmentCommand, List<SearchSubjectAssignmentModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateSubjectAssignmentCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<SearchSubjectAssignmentModel>> Handle(CreateSubjectAssignmentCommand request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await currentUser.GetUserId();
            var SubjectAssignment = request.Id != 0 ? context.SubjectAssignments.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.SubjectAssignment();
            IEnumerable<SearchSubjectAssignmentModel> result = new List<SearchSubjectAssignmentModel>();
            SubjectAssignment.TeacherId = request.TeacherId;
            SubjectAssignment.YearId = request.YearId;
            SubjectAssignment.SubjectManagementId = request.SubjectManagementId;
            SubjectAssignment.ClassManagementId = request.ClassManagementId;
            SubjectAssignment.ClassTypeId = request.ClassTypeId;
            SubjectAssignment.SchoolTypeId = request.SchoolTypeId;
            if (request.Id == 0)
            {
                SubjectAssignment.ModifiedBy = "";
                SubjectAssignment.ModifiedOn = DateTime.Now;
                SubjectAssignment.CreatedBy = CurrentUserId;
                SubjectAssignment.CreatedOn = DateTime.Now;
                context.SubjectAssignments.Add(SubjectAssignment);
            }
            else
            {
                SubjectAssignment.ModifiedBy += "," + CurrentUserId; ;
                SubjectAssignment.ModifiedOn = DateTime.Now;
            }
            await context.SaveChangesAsync();
            result = await mediator.Send(new SearchSubjectAssignmentQuery() { Id = SubjectAssignment.Id });
            return result.ToList();

        }
    }
}
