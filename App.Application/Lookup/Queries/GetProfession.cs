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


    public class GetProfession : IRequest<List<ProfessionModel>>
    {
        public int? Id { get; set; }

    }

    public class SearchProfessionQueryHandler : IRequestHandler<GetProfession, List<ProfessionModel>>
    {
        private AppDbContext Context { get; set; }
        public SearchProfessionQueryHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<ProfessionModel>> Handle(GetProfession request, CancellationToken cancellationToken)
        {
            List<ProfessionModel> list = new List<ProfessionModel>();
            var query = Context.Professions.AsQueryable();

            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }

            list = await(from o in query
                         select new ProfessionModel
                         {
                             Id = o.Id,
                             Name = o.Name,
                             Dari= o.Dari


                         }).ToListAsync(cancellationToken);
            return list;
        }
         
    }
}

