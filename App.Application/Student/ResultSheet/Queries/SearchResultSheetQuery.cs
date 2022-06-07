using App.Application.Student.ResultSheet.Model;
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

namespace App.Application.Student.ResultSheet.Queries
{
   public class SearchResultSheetQuery:IRequest<IEnumerable<ResultSheetModel>>
    {
        public decimal? ID { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public int? ClassTypeId { get; set; }
        public int? SchoolId { get; set; }
        public int? SchoolTypeId { get; set; }
        public int? BirthLocationId { get; set; }
        //public int avg { get; set; }
        //public int Total { get; set; }
        //public string Result { get; set; }
        //public string ClassGrad { get; set; }
        //public int PresentDays { get; set; }
        //public int UpsentDays { get; set; }
        //public int SikDays { get; set; }
        //public int LeaveDays { get; set; }

    }

    public class SearchResultSheetQueryHandler : IRequestHandler<SearchResultSheetQuery, IEnumerable<ResultSheetModel>>
    {
        private AppDbContext context;
        public SearchResultSheetQueryHandler(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ResultSheetModel>> Handle(SearchResultSheetQuery request, CancellationToken cancellationToken)
        {
            var query = context.StudentResultSheets.AsQueryable();
            if(request.ID.HasValue)
            {
                query = query.Where(c => c.Id == request.ID);
            }
            if (request.SchoolId.HasValue)
            {
                query = query.Where(s=> s.SchoolId == request.SchoolId);
            }
            if (request.ClassTypeId.HasValue)
            {
                query = query.Where(t=> t.ClassTypeId == request.ClassTypeId);
            }
            if (request.SchoolTypeId.HasValue)
            {
                query = query.Where(i=> i.SchoolTypeId == request.SchoolTypeId);
            }

            if (!String.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, String.Concat("%", request.FirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }
            return await query.Select(s => new ResultSheetModel
            {
                Id  = s.Id,
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
                StudentClassName = s.StudentClassName,
                SchoolNameEng = s.SchoolNameEng,
                DobShamsi = PersianDate.GetFormatedString(s.DateOfBirth),
                BirthLocationText = context.Locations.Where(b=> b.Id == request.BirthLocationId).Select(b=> b.Dari).Single(),
            }).ToListAsync();
        }
    }
}
