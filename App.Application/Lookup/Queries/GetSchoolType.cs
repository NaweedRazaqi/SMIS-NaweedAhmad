using App.Application.Lookup.Models;
using App.Domain.Entity.look;
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


    public class GetSchoolType : IRequest<List<SchoolType>>
    {
        public int? Id { get; set; }
        public int? SchoolTypeId { get; set; }

    }
    public class SearchSchoolTypeQueryHandler : IRequestHandler<GetSchoolType, List<SchoolType>>
    {
        private AppDbContext Context { get; set; }
        public SearchSchoolTypeQueryHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<SchoolType>> Handle(GetSchoolType request, CancellationToken cancellationToken)
        {
           


            List<SchoolType> list = new List<SchoolType>();
            if (request.Id != null)
                list = await Context.SchoolTypes.Where(c => c.Id == request.Id).ToListAsync();
            else if (request.SchoolTypeId != null)
            {
                list = await Context.SchoolTypes.Where(c => c.Id == request.SchoolTypeId).ToListAsync();
            }
            else
                list = await Context.SchoolTypes.ToListAsync(cancellationToken);

            return list;


        }
    }



}
