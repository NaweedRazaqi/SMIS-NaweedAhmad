using App.Application.Student.Sawaneh.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Sawaneh.Queries
{

    public class SearchStudentSawanehQuery : IRequest<IEnumerable<SawanehSearchModel>>
    {
        public decimal? Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public int? ClassTypeId { get; set; }
        public int? SchoolId { get; set; }
        public int? StudentAssassNumber { get; set; }
    }
    public class SearchStudentSawanehQueryHandler : IRequestHandler<SearchStudentSawanehQuery, IEnumerable<SawanehSearchModel>>
    {
        private AppDbContext context;
        public SearchStudentSawanehQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SawanehSearchModel>> Handle(SearchStudentSawanehQuery request, CancellationToken cancellationToken)
        {
            var query = context.VStudentSawanehs.AsQueryable();
            if (request.Id.HasValue)
            {
                query = query.Where(c => c.Id == request.Id);
            }
            if (request.SchoolId.HasValue)
            {
                query = query.Where(s => s.SchoolId == request.SchoolId);
            }
            if (request.StudentAssassNumber.HasValue)
            {
                query = query.Where(s => s.StudentAssassNumber == request.StudentAssassNumber);
            }
            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(t => t.ClassTypeId == request.ClassTypeId);
            }
            
            if (!String.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, String.Concat("%", request.FirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }

            return await query.Select(s => new SawanehSearchModel
            {
                Id = s.Id,
                FirstName = s.FirstName + " " + "" + s.LastName,
                FatherName = s.FatherName,
                FirstNameEng = s.FirstNameEng,
                LastNameEng = s.LastNameEng,
                LastName = s.LastName,
                GrandFatherName = s.GrandFatherName,
                PhotoPath = s.PhotoPath,
                Code = s.Code,
                FatherNameEng = s.FatherNameEng,
                StudentAssassNumber = s.StudentAssassNumber,
                SchoolId = s.SchoolId,
                ClassTypeId = s.ClassTypeId,
                Classtype = s.Classtype,
                SchoolName = s.SchoolName,
                MotherLanguageId = s.MotherLanguageId,
                MotherLanguage = s.MotherLanguage,
                DateOfBirth = s.DateOfBirth,
                DobShamsi = PersianDate.GetFormatedString(s.DateOfBirth),
                Reasons = s.Reasons,
                Fine = s.Fine,
                TerminationDate = s.TerminationDate,
                StudentHealth = s.StudentHealth,
                RelativeName = s.RelativeName,
                termdateShamsi= PersianDate.GetFormatedString(s.TerminationDate),
                FatherProfession = s.FatherProfession,
                PermenentLocation = s.PermenentLocation,
                TerminatedClass = s.TerminatedClass,
                TdocumentNo = s.TdocumentNo,
                TerminatedClassText = s.TerminatedClassText
            }).ToListAsync();
        }
    }
}

