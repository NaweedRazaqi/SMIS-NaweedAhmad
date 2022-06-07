
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Prf.Queries
{

    public class SearchStudentHealthReportQuery : IRequest<List<StudentHealthReportModel>>
    {
        public int? Id { get; set; }
        public long? ProfileId { get; set; }

    }

    public class StudentHealthReportQueryHandler : IRequestHandler<SearchStudentHealthReportQuery, List<StudentHealthReportModel>>
    {

        private readonly AppDbContext context;
        public StudentHealthReportQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }

        public async Task<List<StudentHealthReportModel>> Handle(SearchStudentHealthReportQuery request, CancellationToken cancellationToken)
        {

            if (!request.Id.HasValue && !request.ProfileId.HasValue)
            {
                return new List<StudentHealthReportModel>();
            }
            var query = context.StudentHealthReports.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(r => r.Id == request.Id);
            }
            if (request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }
            return await query.Include(p => p.Profile)
            .Select(e => new StudentHealthReportModel
            {
                Id = e.Id,
                ProfileId = e.ProfileId,
                Description = e.Description

            }).ToListAsync();
        }
    }
}

