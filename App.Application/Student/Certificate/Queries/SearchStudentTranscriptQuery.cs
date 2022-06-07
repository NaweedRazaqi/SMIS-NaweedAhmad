using App.Application.Student.Certificate.Model;
using App.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Certificate.Queries
{

    public class SearchStudentTranscriptQuery : IRequest<IEnumerable<StudentTranscriptModel>>
    {
        public long? ProfileId { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
    }

    public class SearchStudentTranscriptQueryHandler : IRequestHandler<SearchStudentTranscriptQuery, IEnumerable<StudentTranscriptModel>>
    {
        private AppDbContext context;
        public SearchStudentTranscriptQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentTranscriptModel>> Handle(SearchStudentTranscriptQuery request, CancellationToken cancellationToken)
        {
            var query = context.VStudentsTranscripts.AsQueryable();
            if (request.ProfileId.HasValue)
            {
                query = query.Where(c => c.ProfileId == request.ProfileId);
            }

            return await query.Select(s => new StudentTranscriptModel
            {
               ProfileId = s.ProfileId,
               FirstName = s.FirstName,
               FatherName = s.FatherName,
               StudentSubjects = s.StudentSubjects,
               _10classMark = s._10classMark,
                _11classMark = s._11classMark,
                _12classMark = s._12classMark,


            }).ToListAsync();

        }
    }
}
