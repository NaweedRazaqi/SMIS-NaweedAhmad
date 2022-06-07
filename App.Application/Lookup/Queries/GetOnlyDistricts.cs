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

    public class GetOnlyDistricts : IRequest<List<PdistrictsModel>>
    {
        public int? ID { get; set; }
        public int? ProvinceID { get; set; }
    }

    public class GetOnlyDistrictsHandler : IRequestHandler<GetOnlyDistricts, List<PdistrictsModel>>
    {
        private AppDbContext Context { get; set; }
        public GetOnlyDistrictsHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<PdistrictsModel>> Handle(GetOnlyDistricts request, CancellationToken cancellationToken)
        {
            var query = Context.Pdistricts
              .AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProvinceID.HasValue)
            {
                query = query.Where(e => e.ProvinceId == request.ProvinceID);
            }

            return await query.Select(e => new PdistrictsModel
            {
                Id= e.Id,
                ProvinceId = e.ProvinceId,
                Name = e.Name,
                DistrictCode = e.DistrictCode
            }).ToListAsync();

        }
    }
}
