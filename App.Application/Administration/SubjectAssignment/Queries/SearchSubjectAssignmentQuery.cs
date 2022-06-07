using App.Application.SubjectAssignment.Models;
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

namespace App.Application.SubjectAssignment.Queries
{
    public class SearchSubjectAssignmentQuery : IRequest<IEnumerable<SearchSubjectAssignmentModel>>
    {
        public int? Id { get; set; }
        public int? TeacherId { get; set; }
        public int? YearId { get; set; }
        public long? SubjectManagementId { get; set; }
        public int? ClassTypeId { get; set; }
        public long? ClassManagementId { get; set; }
    }
    public class SearchSubjectAssignmentQueryHandler : IRequestHandler<SearchSubjectAssignmentQuery, IEnumerable<SearchSubjectAssignmentModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchSubjectAssignmentQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }
        public async Task<IEnumerable<SearchSubjectAssignmentModel>> Handle(SearchSubjectAssignmentQuery request, CancellationToken cancellationToken)
        {
            var query = context.SubjectAssignments.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            if (request.ClassManagementId.HasValue)
            {
                query = query.Where(e => e.ClassManagementId == request.ClassManagementId);
            }

            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(e => e.ClassTypeId == request.ClassTypeId);
            }

            if (request.SubjectManagementId.HasValue)
            {
                query = query.Where(e => e.SubjectManagementId == request.SubjectManagementId);
            }
            if (request.YearId.HasValue)
            {
                query = query.Where(e => e.YearId == request.YearId);
            }
            if (request.TeacherId.HasValue)
            {
                query = query.Where(e => e.TeacherId == request.TeacherId);
            }
            return await query.Select(p => new SearchSubjectAssignmentModel
            {
                Id = p.Id,
                TeacherId = p.TeacherId,
                SchoolTypeId = p.SchoolTypeId,
                SubjectManagementId = p.SubjectManagementId,
                ClassManagementId = p.ClassManagementId,
                ClassTypeId = p.ClassTypeId,
                YearId = p.YearId,
                SubjectManagementName = p.SubjectManagement.Name,
                ClassManagementName = p.ClassManagement.Name,
                ClassTypeName = p.ClassType.DariName,
                YearName = p.Year.Name,
                TeacherName = p.Teacher.Name,
                SchoolTypeText = p.SchoolType.NameDari
            }).ToListAsync();
        }
    }
}
