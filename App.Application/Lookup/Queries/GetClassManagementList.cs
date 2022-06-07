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
    public class GetClassManagementList : IRequest<List<ClassManagementModel>>
    {
        public int? Id { get; set; }
    }
    public class GetClassManagementListHandler : IRequestHandler<GetClassManagementList, List<ClassManagementModel>>
    {
        private AppDbContext Context { get; set; }
        public GetClassManagementListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<ClassManagementModel>> Handle(GetClassManagementList request, CancellationToken cancellationToken)
        {
            List<ClassManagementModel> list = new List<ClassManagementModel>();

            var query = Context.ClassManagements.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }


            list = await (from o in query
                          select new ClassManagementModel
                          {
                              Id = o.Id,
                              ClassTypeId=o.ClassTypeId,
                              Name=o.Name,
                              NameEng=o.NameEng,
                              SchoolId=o.SchoolId,
                              Year=o.Year

                            

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }

}

