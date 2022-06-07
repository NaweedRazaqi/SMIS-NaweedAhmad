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
    public class GetSchoolList: IRequest<List<SearchSchoolModel>>
    {
        public int? Id { get; set; }
        public short? SchoolCategoryId { get; set; }
        public short? SchoolTypeId { get; set; }
    }
    public class GetSchoolListHandler : IRequestHandler<GetSchoolList, List<SearchSchoolModel>>
    {
        private AppDbContext Context { get; set; }
        public GetSchoolListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<SearchSchoolModel>> Handle(GetSchoolList request, CancellationToken cancellationToken)
        {
            List<SearchSchoolModel> list = new List<SearchSchoolModel>();

            var query = Context.Schools.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }
            if (request.SchoolCategoryId.HasValue)
            {
                query = query.Where(o => o.SchoolCategoryId == request.SchoolCategoryId);
            }
            if (request.SchoolTypeId.HasValue)
            {
                query = query.Where(o => o.SchoolTypeId == request.SchoolTypeId);
            }

            list = await (from o in query
                          select new SearchSchoolModel
                          {
                              Id = o.Id,
                              Code = o.Code,
                              Name = o.Name,
                              Dari = o.Dari,
                              Pashto = o.Pashto,
                              SchoolTypeId = o.SchoolTypeId,
                              SchoolCategoryId = o.SchoolCategoryId

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
