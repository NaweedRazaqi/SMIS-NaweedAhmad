using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public  class GetServiceList : IRequest<List<ServiceModel>>
    {
        public int? ID { get; set; }
    }

    public class GetServiceListHandler : IRequestHandler<GetServiceList, List<ServiceModel>>
    {
        
        private AppDbContext Context { get; set; }
        public GetServiceListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<ServiceModel>> Handle(GetServiceList request, CancellationToken cancellationToken)
        {
            var query = Context.Services.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.ID == request.ID);
            }
            return await query.Select(e => new ServiceModel
            {
                ID = e.ID,
                Name = e.Name,
                NameDari=e.NameDari,
                NamePashto=e.NamePashto
            }).ToListAsync();
        }
    
    }
}
