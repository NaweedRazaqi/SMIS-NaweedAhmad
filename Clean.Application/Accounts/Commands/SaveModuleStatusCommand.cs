using App.Persistence.Context;
using App.Persistence.Models;
using Clean.Application.Accounts.Models;
using Clean.Application.Accounts.Queries;
using Clean.Persistence.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Commands
{
    public class SaveModuleStatusCommand : IRequest<List<ModuleActivation>>
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int? Sorter { get; set; }
        public Boolean IsActive { get; set; }

    }


    public class SaveModuleStatusCommandHandler : IRequestHandler<SaveModuleStatusCommand, List<ModuleActivation>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public SaveModuleStatusCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;
        }

        public async Task<List<ModuleActivation>> Handle(SaveModuleStatusCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<ModuleActivation> result = new List<ModuleActivation>();
            var modulestatus = request.Id!= 0 ? context.Modules.Where(m => m.Id == request.Id).Single() : new Domain.Entity.look.Module();
            int CurrentUserId = await currentUser.GetUserId();
            modulestatus.Id = request.Id;
            modulestatus.Name = request.Name;
            modulestatus.Description = modulestatus.Description;
            modulestatus.IsActive = request.IsActive;

            await context.SaveChangesAsync();

            result = await mediator.Send(new GetActivationQuery() { Id = modulestatus.Id });

            return result.ToList();

        }
       
    }

}
