using App.Persistence.Context;
using Clean.Application.Accounts.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Accounts.Queries
{


    public class GetActivationQuery : IRequest<List<ModuleActivation>>
    {
        public int? Id { get; set; }
    }

    public class GetActivationQueryHandler : IRequestHandler<GetActivationQuery, List<ModuleActivation>>
    {
        public GetActivationQueryHandler(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private AppDbContext AppDbContext { get; }

        public async Task<List<ModuleActivation>> Handle(GetActivationQuery request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.Modules.AsQueryable();
            if(request.Id != null)
            {
                query = query.Where(q => q.Id == request.Id);
            }
            return await query.Select(q => new ModuleActivation
            {
                Id = q.Id,
                Name = q.Name,
                Description = q.Description,
                Sorter = q.Sorter,
                IsActive = q.IsActive,
                IsActiveName = q.IsActive == true ? "فعال" : "غیر فعال"

            }).ToListAsync();
        }
    }
}
