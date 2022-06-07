using App.Persistence.Context;
using Clean.Application.Lookup.Models;
using Clean.Persistence.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Lookup.Queries
{
    public class GetModuleStausList : IRequest<List<ModuleStatusModel>>
    {
        public int? Id { get; set; }
    }
    public class GetModuleStausListHandler : IRequestHandler<GetModuleStausList, List<ModuleStatusModel>>
    {


        private AppDbContext AppDbContext { get; }

        public GetModuleStausListHandler(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public async Task<List<ModuleStatusModel>> Handle(GetModuleStausList request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.Modules.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(M => M.Id == request.Id);
            }
            return await query.Select( q => new ModuleStatusModel { 
                Id= q.Id,
                IsActive = q.IsActive,
                IsActiveName = ( q.IsActive == true ? "فعال" : "غیر فعال")
            }).ToListAsync();
        }
    }
}
