using App.Persistence.Context;
using Clean.Application.System.Models;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.System.Queries
{
    public class SeachOwnerQuery : IRequest<IEnumerable<SearchOwnerModel>>
    {

        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }


    public class SeachOwnerQueryHandler : IRequestHandler<SeachOwnerQuery, IEnumerable<SearchOwnerModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SeachOwnerQueryHandler(AppDbContext context, ICurrentUser currentUser)
        {
            this.context = context;
            User = currentUser;
        }

        public async Task<IEnumerable<SearchOwnerModel>> Handle(SeachOwnerQuery request, CancellationToken cancellationToken)
        {
            var query = context.AspNetOwners.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }
            if (!String.IsNullOrEmpty(request.Name))
            {
                query = query.Where(e => EF.Functions.Like(e.Name, String.Concat("%", request.Name, "%")));
            }

            return (await query.OrderBy(x => x.Id).Select(O => new SearchOwnerModel
            {
                Id = O.Id,
                Name = O.Name,
                ParentId = O.ParentId,
                IsActive = O.IsActive,
                ProvinceId = O.ProvinceId,
                ParentName = context.AspNetOwners.Where(o=> o.Id == request.ParentId).Select(w=> w.Name).SingleOrDefault(),
                IsActiveText = O.IsActive == true ? "فعال" : "غیر فعال",
                ProvinceText = O.Province.Title
            }).ToListAsync());
        }
    }
}

