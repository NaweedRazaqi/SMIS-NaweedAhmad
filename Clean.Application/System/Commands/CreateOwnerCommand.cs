using App.Persistence.Context;
using Clean.Application.System.Models;
using Clean.Application.System.Queries;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Commands
{
     

    public class CreateOwnerCommand : IRequest<List<SearchOwnerModel>>
    {
        [MinLength(4, ErrorMessage = "نام موقف حداقل باید دارای چهار حرف باشد")]
        [MaxLength(32, ErrorMessage = "نام موقف حد اکثر میتواند دارای سی و دو حرف باشد")]
        [Required]
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public short? ProvinceId { get; set; }

    }

    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, List<SearchOwnerModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public CreateOwnerCommandHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;
        }

        public async Task<List<SearchOwnerModel>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<SearchOwnerModel> OwResult = new List<SearchOwnerModel>();

          ///Domain.Entity.Owner.AspNetOwner Owner = request.Id != 0 ? context.AspNetOwners.Where(m => m.Id == request.Id).SingleOrDefault() : new Clean.Domain.Entity.Owner.AspNetOwner();
            var Owner = request.Id.HasValue ? context.AspNetOwners.Where(e => e.Id == request.Id).Single() : new Domain.Entity.Owner.AspNetOwners();
            int CurrentUserId = await currentUser.GetUserId();

            Owner.Name = request.Name;
            Owner.ParentId = request.ParentId;
            Owner.IsActive = request.IsActive;
            Owner.ProvinceId = request.ProvinceId;
            if (request.Id.HasValue)
            {
                Owner.ModifiedOn = DateTime.Now; ;
                Owner.ModifiedBy = "," + CurrentUserId;

            }
            else
            {

                Owner.CreatedBy = CurrentUserId;
                Owner.CreatedOn = DateTime.Now;
                context.AspNetOwners.Add(Owner);
            }
            await context.SaveChangesAsync();

            OwResult = await mediator.Send(new SeachOwnerQuery() { Id = Owner.Id });

            return OwResult.ToList();
        }

    }
}


