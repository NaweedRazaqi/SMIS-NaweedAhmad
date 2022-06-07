using App.Persistence.Context;
using Clean.Application.Lookup.Models;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Lookup.Queries
{
    public class GetUserNameList : IRequest<List<SearchUserNameModel>>
    {
        public int? Id { get; set; }
    }

    public class GetUserNameListHandler : IRequestHandler<GetUserNameList, List<SearchUserNameModel>>
    {

        private readonly AppIdentityDbContext _dbContext;

       
        public GetUserNameListHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SearchUserNameModel>> Handle(GetUserNameList request, CancellationToken cancellationToken)
        {
            //var query = dbContext.user.AsQueryable();
            var query = _dbContext.Users.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(O => O.Id == request.Id);
            }
            return await query.Select(q => new SearchUserNameModel
            {
                Id = q.Id,
                UserName = q.UserName,
                FatherName = q.FatherName,
                LastName = q.LastName,
                Email = q.Email
            }).ToListAsync();
        }
    }
}
