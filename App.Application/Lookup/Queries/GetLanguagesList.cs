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

    public class GetLanguagesList : IRequest<List<LanguagesModel>>
    {
        public int? Id { get; set; }

    }

    public class GetLanguagesListHandler : IRequestHandler<GetLanguagesList, List<LanguagesModel>>
    {
        private AppDbContext Context { get; set; }
        public GetLanguagesListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<LanguagesModel>> Handle(GetLanguagesList request, CancellationToken cancellationToken)
        {
            List<LanguagesModel> list = new List<LanguagesModel>();
            var query = Context.Languages.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }

            list = await(from o in query
                         select new LanguagesModel
                         {
                             Id = o.Id,
                             Name = o.Name


                         }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
