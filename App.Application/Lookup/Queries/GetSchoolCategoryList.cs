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


    public class GetSchoolCategoryList : IRequest<List<SearchSchoolCategoryModel>>
    {
        public int? Id { get; set; }
    }
    public class GetSchoolCategoryListHandler : IRequestHandler<GetSchoolCategoryList, List<SearchSchoolCategoryModel>>
    {
        private AppDbContext Context { get; set; }
        public GetSchoolCategoryListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<SearchSchoolCategoryModel>> Handle(GetSchoolCategoryList request, CancellationToken cancellationToken)
        {
            List<SearchSchoolCategoryModel> list = new List<SearchSchoolCategoryModel>();
            var query = Context.SchoolCategories.AsQueryable();
            if (request.Id != null)
            {
                
                query = query.Where(o => o.Id == request.Id);
            }
              
            list = await(from o in query
                             select new SearchSchoolCategoryModel
                             {
                                 Id = o.Id,
                                 Name = o.Name,

                             }).ToListAsync(cancellationToken);
                return list;
            }

        }
    }

