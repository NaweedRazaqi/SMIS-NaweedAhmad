using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
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
    public class GetSubjectLists : IRequest<List<SubjectManagementModel>>
    {

        public int? Id { get; set; }
        public int? SchoolTypeID { get; set; }
    }
    public class GetSubjectListsHandler : IRequestHandler<GetSubjectLists, List<SubjectManagementModel>>
    {
        private AppDbContext Context { get; set; }
        public GetSubjectListsHandler(AppDbContext context)
        {
            Context = context;
        }

        public async Task<List<SubjectManagementModel>> Handle(GetSubjectLists request, CancellationToken cancellationToken)
        {
            var query = Context.SubjectManagements.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            else if (request.SchoolTypeID.HasValue)
            {
                query = query.Where(e => e.SchoolTypeId == request.SchoolTypeID).AsQueryable();
            }
                return await query.Select(e => new SubjectManagementModel

                {
                    Id = e.Id,
                    Name = e.Name,
                    SchoolTypeId = e.SchoolTypeId
                }

            ).ToListAsync();
            }
        }
    }

