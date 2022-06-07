using App.Persistence.Context;
using Clean.Application.Lookup.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Lookup.Queries
{
    public class GetOwnerParentList : IRequest<List<GetOwnerParentModel>>
    {
        public int? Id { get; set; }
    }

    public class GetOwnerParentListHandler : IRequestHandler<GetOwnerParentList, List<GetOwnerParentModel>>
    {

        private readonly AppDbContext context;

        public GetOwnerParentListHandler(AppDbContext context )
        {
            this.context = context;
        }
        public async Task<List<GetOwnerParentModel>> Handle(GetOwnerParentList request, CancellationToken cancellationToken)
        {
            var query = context.AspNetOwners.AsQueryable();
            if (request.Id != null)
            {
                query = query.Where(O => O.Id == request.Id);
            }
            return await query.Select(q => new GetOwnerParentModel
            {
                Id = q.Id,
                Name = q.Name,
                ParentId = q.ParentId,
                IsActive = q.IsActive,
            }).ToListAsync();
        }
    }
    
  }
