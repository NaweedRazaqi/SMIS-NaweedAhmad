using App.Persistence.Context;
using Clean.Application.System.Models;
using Clean.Common.Exceptions;
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
    public class SearchAssignOwnerQuery : IRequest<IEnumerable<SearchAssignOwnerModel>>
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? OwnerId { get; set; }
    }

    public class SearchAssignOwnerQueryHandler : IRequestHandler<SearchAssignOwnerQuery, IEnumerable<SearchAssignOwnerModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private readonly AppIdentityDbContext _context;

        public SearchAssignOwnerQueryHandler(AppDbContext context, AppIdentityDbContext dbContext, ICurrentUser currentUser)
        {
            this.context = context;
            User = currentUser;
           this. _context = dbContext;
        }

        public async Task<IEnumerable<SearchAssignOwnerModel>> Handle(SearchAssignOwnerQuery request, CancellationToken cancellationToken)
        {

            if (request.UserId == null & request.Id == null)
            {
                throw new BusinessRulesException("انتخاب کاربر الزامی میباشد لطفا کابر را انتخاب نموده و جستجو نمایید!");
            }
            else
            {


                var query = context.UserOwners.AsQueryable();

                if (request.Id.HasValue)
                {
                    query = query.Where(e => e.Id == request.Id.Value);
                }
                if (request.UserId.HasValue)
                {
                    query = query.Where(f => f.UserId == request.UserId);
                }
                if (request.OwnerId.HasValue)
                {
                    query = query.Where(o => o.OwnerId == request.OwnerId);
                }
                var UserTxt = _context.Users.Where(u => u.Id == request.UserId).Select(o => o.UserName + " " + o.LastName).Single();
                return (await query.OrderBy(x => x.Id).Select(O => new SearchAssignOwnerModel
                {
                    Id = O.Id,
                    UserId = O.UserId,
                    OwnerId = O.OwnerId,
                    IsActive = O.IsActive,
                    IsActiveText = O.IsActive == true ? "فعال" : "غیر فعال",
                    UserText = UserTxt,
                    OwnerText = O.Owner.Name
                }).ToListAsync());
            }
        }
    }
}