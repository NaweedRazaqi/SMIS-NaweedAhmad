using App.Application.Administration.Subject.Models;
using App.Persistence.Context;
using Clean.Persistence.Identity;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Administration.Subject.Queries
{
    public class SearchSubjectAssigmentQueries : IRequest<IEnumerable<SearchSubjectAssignmentModel>>
    {
        public long? Id { get; set; }
        public long? SubjectId { get; set; }
        public int? ClassTypeId { get; set; }
        public short? SchoolTypeId { get; set; }
    }

    public class SearchSubjectAssigmentQueriesHandler : IRequestHandler<SearchSubjectAssigmentQueries, IEnumerable<SearchSubjectAssignmentModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchSubjectAssigmentQueriesHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }

        public async Task<IEnumerable<SearchSubjectAssignmentModel>> Handle(SearchSubjectAssigmentQueries request, CancellationToken cancellationToken)
        {
            var query = context.ClassSubjectManagements.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }

            if (request.SubjectId.HasValue)
            {
                query = query.Where(e => e.SubjectId == request.SubjectId);
            }
            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(e => e.ClassTypeId == request.ClassTypeId);
            }
            return await query.Select(p => new SearchSubjectAssignmentModel
            {
                Id = p.Id,
                SubjectId = p.SubjectId,
                SubjectText = p.Subject.Name,
                ClassTypeId = p.ClassTypeId,
                ClasstypeIdText = p.ClassType.DariName,
                SchoolTypeId = p.SchoolTypeId,
                SchoolTypeName = p.SchoolType.NameDari
            }).ToListAsync();
        }
    }
}
