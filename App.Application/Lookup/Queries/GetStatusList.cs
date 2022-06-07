using App.Application.Lookup.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Lookup.Queries
{
    public class GetStatusList : IRequest<List<StatusModel>>
    {
        public int? ID { get; set; }
    }
    public class GetStatusListHandler : IRequestHandler<GetStatusList, List<StatusModel>>
    {
        private AppDbContext Context { get; set; }
        public GetStatusListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<StatusModel>> Handle(GetStatusList request, CancellationToken cancellationToken)
        {
            var query = Context.Status.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Where(S => S.Category == "SM").Select(e => new StatusModel
            {
                Id = e.Id,
                Name = e.Name,
                Dari = e.Dari,
            }).ToListAsync();
        }
    }
}
