using App.Persistence.Context;
using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Commands
{


    public class AssignUserOwnerCommand : IRequest<List<SearchAssignOwnerModel>>
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public bool? IsActive { get; set; }

    }

    public class AssignUserOwnerCommandHandler : IRequestHandler<AssignUserOwnerCommand, List<SearchAssignOwnerModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public AssignUserOwnerCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;
        }

        public async Task<List<SearchAssignOwnerModel>> Handle(AssignUserOwnerCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<SearchAssignOwnerModel> OwResult = new List<SearchAssignOwnerModel>();
            var Owner = request.Id.HasValue ? context.UserOwners.Where(e => e.Id == request.Id).Single() : new Domain.Entity.Owner.UserOwner();
            int CurrentUserId = await currentUser.GetUserId();

            Owner.UserId = request.UserId;
            Owner.OwnerId = request.OwnerId;
            Owner.IsActive = request.IsActive;
            if (request.Id.HasValue)
            {
                Owner.ModifiedOn = DateTime.Now; ;
                Owner.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                Owner.CreatedBy = CurrentUserId;
                Owner.CreatedOn = DateTime.Now;
                context.UserOwners.Add(Owner);
            }
            await context.SaveChangesAsync();

            OwResult = await mediator.Send(new SearchAssignOwnerQuery() { Id = Owner.Id, UserId = request.UserId });

            return OwResult.ToList();
        }
    }
}
