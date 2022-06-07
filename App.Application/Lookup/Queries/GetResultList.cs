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
   public class GetResultList : IRequest<List<ResultModel>>
    {

        public int? Id { get; set; }

    }


    public class GetResultListHandler : IRequestHandler<GetResultList, List<ResultModel>>
    {
        private AppDbContext Context { get; set; }
        public GetResultListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<ResultModel>> Handle(GetResultList request, CancellationToken cancellationToken)
        {
            List<ResultModel> list = new List<ResultModel>();

            var query = Context.Results.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }


            list = await (from o in query
                          select new ResultModel
                          {
                              Id = o.Id,
                              Name=o.Name,
                              DariName=o.DariName,
                              PashtoName=o.PashtoName
                            

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
