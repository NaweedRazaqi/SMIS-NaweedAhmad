using App.Application.Subject.Models;
using App.Application.Subject.Queries;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Subject.Commands
{
    public class CreateSubjectCommand : IRequest<List<SearchSubjectModel>>
    {
        public long? Id { get; set; }
        public String Name{ get; set; }
        public String NameEng{ get; set; }
        public String? Remarks { get; set; }
        public short? StatusId { get; set; }
        public int? ViewOrder { get; set; }
        public short SchoolTypeId { get; set; }

    }
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, List<SearchSubjectModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateSubjectCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<SearchSubjectModel>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            int CurrentUserId = await currentUser.GetUserId();
            var Subject = request.Id.HasValue ? context.SubjectManagements.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.SubjectManagement();
            IEnumerable<SearchSubjectModel> result = new List<SearchSubjectModel>();
          
            Subject.Name = request.Name.Trim();
            Subject.NameEng = request.NameEng.Trim();
            Subject.Remarks = request.Remarks;
            Subject.StatusId = request.StatusId.Value;
            Subject.ViewOrder = request.ViewOrder;
            Subject.SchoolTypeId = request.SchoolTypeId;

            if (!request.Id.HasValue)
            {
                Subject.CreatedBy = CurrentUserId;
                Subject.CreatedOn = DateTime.Now;
                context.SubjectManagements.Add(Subject);
            }
            else
            {
                Subject.ModifiedBy += "," + CurrentUserId; ;
                Subject.ModifiedOn = DateTime.Now;
            }
            await context.SaveChangesAsync();
            result = await mediator.Send(new SearchSubjectQuery() { Id = Subject.Id });
            return result.ToList();

        }
    }
}
