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
    public class GetClassTypeList: IRequest<List<ClassTypeModel>>
    {
        public int? Id { get; set; }
    }
    public class GetClassTypeListHandler : IRequestHandler<GetClassTypeList, List<ClassTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetClassTypeListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<ClassTypeModel>> Handle(GetClassTypeList request, CancellationToken cancellationToken)
        {
            List<ClassTypeModel> list = new List<ClassTypeModel>();

            var query = Context.ClassTypes.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }
            list = await (from o in query
                          select new ClassTypeModel
                          {
                             Id=o.Id,
                             Name=o.Name,
                             DariName=o.DariName,
                             PashtoName=o.PashtoName

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
