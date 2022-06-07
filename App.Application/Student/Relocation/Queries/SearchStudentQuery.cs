using App.Application.Student.Relocation.Models;
using App.Persistence.Context;
using Clean.Common.Dates;
using Clean.Common.Service;
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

namespace App.Application.Student.Relocation.Queries
{

    public class SearchStudentQuery : IRequest<IEnumerable<SearchStudentModel>>

    {
        public decimal? ID { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public decimal? ProfileId { get; set; }


    }



    public class SearchStudentQueryHandler : IRequestHandler<SearchStudentQuery, IEnumerable<SearchStudentModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchStudentQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }

        public async Task<IEnumerable<SearchStudentModel>> Handle(SearchStudentQuery request, CancellationToken cancellationToken)
        {


            //if (!request.ID.HasValue && !request.ProfileId.HasValue)
            //{
            //    return new List<SearchStudentModel>();
            //}
            var query = context.Profiles.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            
            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }

            if (!String.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, String.Concat("%", request.FirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }

            return (await query.Select(p => new SearchStudentModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                FatherName = p.FatherName,
                FirstNameEng = p.FirstNameEng,
                LastNameEng = p.LastNameEng,
                FatherNameEng = p.FatherNameEng,
                GenderId = p.GenderId,
                DateOfBirth = p.DateOfBirth,
                GrandFatherName = p.GrandFatherName,
                PhotoPath = p.PhotoPath,
                GenderText = p.Gender.Name,
                Code = p.Code,
                NID = p.NationalId,
                DocumentTypeId = p.DocumentTypeId,
                Province = p.Province,
                ProvinceText = p.ProvinceNavigation.Dari,
                District = p.District,
                DistrictText = p.DistrictNavigation.Dari,
                MotherLanguageId = p.MotherLanguageId,
                MotherLanguageName = p.Languages.Name,
                BirthLocationName = p.ProvinceNavigation.Dari,
                QuranChapter = context.QuranChapterMemorizes.Where(q => q.ProfileId == p.Id).Select(f => f.Chapter).Single(),
                DobShamsi = PersianDate.GetFormatedString(p.DateOfBirth),
                Age = (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(p.DateOfBirth.Year)).ToString() + " ساله",
                ClassName = context.StudentRegisterations.Where(R => R.ProfileId == p.Id).Select(S => S.ClassType.DariName).Single(),
                StudentAssasnumber = context.StudentRegisterations.Where(S=> S.ProfileId== p.Id).Select(A=> A.StudentAssassNumber).Single(),
                SchoolName = context.StudentRegisterations.Where(N=> N.ProfileId == p.Id).Select(N=> N.School.Dari).Single(),
                SchoolId = context.StudentRegisterations.Where(N=> N.ProfileId == p.Id).Select(N=> N.School.Id).SingleOrDefault(),
                ClassTypeName = context.StudentRegisterations.Where(C=> C.ProfileId == p.Id).Select(C=> C.ClassType.DariName).SingleOrDefault(),
                ClassTypeId = context.StudentRegisterations.Where(s=> s.ProfileId == p.Id).Select(c=> c.ClassType.Id).SingleOrDefault(),
            }).Take(10).ToListAsync()).Select(cur =>
            {
                cur.NIDText = NationalIDReader.ConvertJSONToString(cur.NID, cur.DocumentTypeText);
                return cur;
            }).ToList();
        }
    }
}
