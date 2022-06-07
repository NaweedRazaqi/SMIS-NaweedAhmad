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
    public class GetSchoolLists : IRequest<List<SearchSchoolModel>>
    {
        public int? Id { get; set; }

        public int? SchoolTypeID { get; set; }
        public short? SchoolCategoryId { get; set; }
    }
    public class GetSchoolListsHandler : IRequestHandler<GetSchoolLists, List<SearchSchoolModel>>
    {
        private AppDbContext Context { get; set; }
        public GetSchoolListsHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<SearchSchoolModel>> Handle(GetSchoolLists request, CancellationToken cancellationToken)
        {
            var query = Context.Schools.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id);
            }
             if (request.SchoolTypeID.HasValue)
            {
                query = query.Where(e => e.SchoolTypeId == request.SchoolTypeID).AsQueryable();

            }
            if (request.SchoolCategoryId.HasValue)
            {
                query = query.Where(e => e.SchoolCategoryId == request.SchoolCategoryId).AsQueryable();

            }
            return await query.Select(e => new SearchSchoolModel

            {
                Id = e.Id,
                Name = e.Name,
                Dari = e.Dari,
                Pashto=e.Pashto,
                Code=e.Code,
                StatusId=e.StatusId,
                SchoolTypeId=e.SchoolTypeId,
                SchoolCategoryId = e.SchoolCategoryId
                }

            ).ToListAsync();
        }
    }
}
