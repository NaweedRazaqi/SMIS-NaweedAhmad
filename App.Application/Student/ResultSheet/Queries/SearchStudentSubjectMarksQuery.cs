using App.Application.Student.ResultSheet.Model;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.ResultSheet.Queries
{


    public class SearchStudentSubjectMarksQuery : IRequest<IEnumerable<vStudentSubjectMarksModel>>
    {
        public long? MarksId { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string StudentSchool { get; set; }
        public string StudentClass { get; set; }
        public long? ProfileId { get; set; }
    }

    public class SearchStudentSubjectMarksQueryHandler : IRequestHandler<SearchStudentSubjectMarksQuery, IEnumerable<vStudentSubjectMarksModel>>
    {
        private AppDbContext context;
        public SearchStudentSubjectMarksQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<vStudentSubjectMarksModel>> Handle(SearchStudentSubjectMarksQuery request, CancellationToken cancellationToken)
        {

            var query = context.StudentSubjectMarks.AsQueryable();
            if (request.MarksId.HasValue)
            {
                query = query.Where(c => c.MarksId == request.MarksId);
            }
            
            if(request.ProfileId.HasValue)
            {
                query = query.Where(p => p.ProfileId == request.ProfileId);
            }

            return await query.Select(s => new vStudentSubjectMarksModel
            {
                MarksId = s.MarksId,
                Marks = s.Marks,
                StudentClass = s.StudentClass,
                StudentSchool = s.StudentSchool,
                FirstName = s.FirstName,
                FatherName = s.FatherName,
                StudentSubjects = s.StudentSubjects,
                StudentExamType = s.StudentExamType,
                ProfileId = s.ProfileId

            }).ToListAsync();
        }
    }
}

