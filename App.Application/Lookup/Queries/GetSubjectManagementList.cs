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
    public class GetSubjectManagementList : IRequest<List<SubjectManagementModel>>
    {
        public int? ID { get; set; }
    }
    public class GetSubjectManagementListHandler : IRequestHandler<GetSubjectManagementList, List<SubjectManagementModel>>
    {
        private AppDbContext Context { get; set; }
        public GetSubjectManagementListHandler(AppDbContext context)
        {
            Context = context;
        }
        public async Task<List<SubjectManagementModel>> Handle(GetSubjectManagementList request, CancellationToken cancellationToken)
        {
            var query = Context.SubjectManagements.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }

            return await query.Select(e => new SubjectManagementModel
            {
                Id = e.Id,
                Name = e.Name,
            }).ToListAsync();
        }
    }
}
