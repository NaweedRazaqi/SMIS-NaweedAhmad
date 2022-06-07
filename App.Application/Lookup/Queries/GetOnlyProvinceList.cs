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

    public class GetOnlyProvinceList : IRequest<List<ProvinceModel>>
    {
        public int? ID { get; set; }
        public string Code { get; set; }
    }
    public class GetOnlyProvinceListHandler : IRequestHandler<GetOnlyProvinceList, List<ProvinceModel>>
    {
        private AppDbContext Context { get; set; }
        public GetOnlyProvinceListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<ProvinceModel>> Handle(GetOnlyProvinceList request, CancellationToken cancellationToken)
        {
            var query = Context.Provinces.AsQueryable();;
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => EF.Functions.ILike(e.Code, String.Concat("%", request.Code, "%")));
            }
            return await query.Select(e => new ProvinceModel
            {
                ID = e.Id,
                Title = e.Title,
                TitleEn = e.TitleEn,
                Code = e.Code,
            }).ToListAsync();
        }
    }
}
