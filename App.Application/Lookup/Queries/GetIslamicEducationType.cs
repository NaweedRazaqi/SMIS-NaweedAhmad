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

    public class GetIslamicEducationType : IRequest<List<IslamicEducationTypeModel>>
    {
        public int? Id { get; set; }

    }

    public class GetIslamicEducationTypeHandler : IRequestHandler<GetIslamicEducationType, List<IslamicEducationTypeModel>>
    {
        private AppDbContext Context { get; set; }
        public GetIslamicEducationTypeHandler(AppDbContext context)
        {
            Context = context;
        }

        public async  Task<List<IslamicEducationTypeModel>> Handle(GetIslamicEducationType request, CancellationToken cancellationToken)
        {
            List<IslamicEducationTypeModel> list = new List<IslamicEducationTypeModel>();
            var query = Context.IslamicEducationTypes.AsQueryable();

            if (request.Id != null)
            {
                query = query.Where(e => e.Id == request.Id);
            }

            list = await(from e in query 
                         select new IslamicEducationTypeModel
                         {
                          Id = e.Id,
                          Name = e.Name,
                          Dari = e.Dari


                         }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
