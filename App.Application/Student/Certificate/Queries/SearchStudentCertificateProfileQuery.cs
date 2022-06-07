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

    public class SearchStudentCertificateProfileQuery : IRequest<IEnumerable<StudentCertificateProfileModel>>
    {
        public decimal? ID { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public int? ClassTypeId { get; set; }
        public int? SchoolId { get; set; }
        public int? SchoolTypeId { get; set; }
    }

    public class SearchStudentCertificateProfileQueryHandler : IRequestHandler<SearchStudentCertificateProfileQuery, IEnumerable<StudentCertificateProfileModel>>
    {
        private AppDbContext context;
        public SearchStudentCertificateProfileQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentCertificateProfileModel>> Handle(SearchStudentCertificateProfileQuery request, CancellationToken cancellationToken)
        {
            var query = context.StudentResultSheets.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(c => c.Id == request.ID);
            }
            if (request.SchoolId.HasValue)
            {
                query = query.Where(s => s.SchoolId == request.SchoolId);
            }
            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(t => t.ClassTypeId == request.ClassTypeId);
            }
            if (request.SchoolTypeId.HasValue)
            {
                query = query.Where(i => i.SchoolTypeId == request.SchoolTypeId);
            }

            if (!String.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, String.Concat("%", request.FirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }

            return await query.Select(s => new StudentCertificateProfileModel
            {
                Id = s.Id,
                FirstName = s.FirstName + " " + "" + s.LastName,
                FatherName = s.FatherName,
                StudentSchoolName = s.StudentSchoolName,
                StudentSchoolType = s.StudentSchoolType,
                FirstNameEng = s.FirstNameEng,
                FatherNameEng = s.FatherNameEng,
                LastNameEng = s.LastNameEng,
                LastName = s.LastName,
                PhotoPath = s.PhotoPath,
                Code = s.Code,
                StudentAssassNumber = s.StudentAssassNumber,
                SchoolId = s.SchoolId,
                SchoolTypeId = s.SchoolTypeId,
                ClassTypeId = s.ClassTypeId,
                ClassManagementId = s.ClassManagementId,
                StudentClassName = s.StudentClassName

            }).ToListAsync();
        }
    }
}
