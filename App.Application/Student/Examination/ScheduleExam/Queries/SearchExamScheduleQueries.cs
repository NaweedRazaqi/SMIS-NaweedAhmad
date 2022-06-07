using App.Application.Student.ScheduleExam.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
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

namespace App.Application.Student.ScheduleExam.Queries
{
    public class SearchExamScheduleQueries : IRequest<IEnumerable<SearchScheduleExamModel>>
    {
        public int? Id { get; set; }
        public string ExmName { get; set; }
        public long? SubjectId { get; set; }
        public int ClassTypeId { get; set; }
        public short? SchoolTypeId { get; set; }
    }
    public class SearchExamScheduleQueriesHandler : IRequestHandler<SearchExamScheduleQueries, IEnumerable<SearchScheduleExamModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchExamScheduleQueriesHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }

        public async Task<IEnumerable<SearchScheduleExamModel>> Handle(SearchExamScheduleQueries request, CancellationToken cancellationToken)
        {
            var query = context.ScheduleExams.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }

            if (!String.IsNullOrEmpty(request.ExmName))
            {
                query = query.Where(e => e.ExmName == request.ExmName);
            }
            if (request.SchoolTypeId.HasValue)
            {
                query = query.Where(q => q.SchoolTypeId == request.SchoolTypeId);
            }

            if (request.SubjectId.HasValue)
            {
                query = query.Where(q => q.SubjectId == request.SubjectId);
            }
            return (await query.OrderBy(x => x.Id).Select(SC => new SearchScheduleExamModel
            {
                Id = SC.Id,
                ExmName = SC.ExmName,
                ClassTypeId = SC.ClassTypeId,
                ClassManagementId = SC.ClassManagementId,
                ExamTimeStart = SC.ExamTimeStart,
                ExamTimeEnd = SC.ExamTimeEnd,
                ExamDate = SC.ExamDate,
                SubjectId = SC.SubjectId,
                dateShamsi = PersianDate.GetFormatedString(SC.ExamDate),
                SubjectText = SC.Subject.Name,
                ClassmanagementText = SC.ClassManagement.Name,
                ClasstypeText = SC.ClassType.DariName,
                SchoolTypeId = SC.SchoolTypeId,
                SchoolTypeText = SC.SchoolType.NameDari
            }).ToListAsync()) ;

        }
    }
}
