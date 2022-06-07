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
    public class GetYearList : IRequest<List<YearModel>>
    {
        public int? ID { get; set; }
    }
    public class GetYearListHandler : IRequestHandler<GetYearList, List<YearModel>>
    {
        private AppDbContext Context { get; set; }
        public GetYearListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<YearModel>> Handle(GetYearList request, CancellationToken cancellationToken)
        {
            var query = Context.Years.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new YearModel
            {
                Id = e.Id,
                Name = e.Name,
                Dari = e.Dari,
            }).ToListAsync();
        }
    }
}
