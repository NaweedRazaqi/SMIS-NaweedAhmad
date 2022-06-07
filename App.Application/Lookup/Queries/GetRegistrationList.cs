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
    public class GetRegistrationList : IRequest<List<RegistrationModel>>
    {
        public int? Id { get; set; }
    }
    public class GetRegistrationListHandler : IRequestHandler<GetRegistrationList, List<RegistrationModel>>
    {
        private AppDbContext Context { get; set; }
        public GetRegistrationListHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<RegistrationModel>> Handle(GetRegistrationList request, CancellationToken cancellationToken)
        {
            List<RegistrationModel> list = new List<RegistrationModel>();

            var query = Context.RegistrationType.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(o => o.Id == request.Id);
            }
            

            list = await (from o in query
                          select new RegistrationModel
                          {
                              Id = o.Id,
                              Name=o.Name,
                              

                          }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
