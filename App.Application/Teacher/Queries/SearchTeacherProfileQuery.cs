using App.Application.Prf.Models;
using App.Application.Teacher.Models;
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

namespace App.Application.Prf.Queries.SearchTeacher
{
    public class SearchTeacherProfileQuery : IRequest<IEnumerable<TeacherModel>>
    {

        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public int? ProcessID { get; set; }
        public int? ServiceTypeId { get; set; }
        public bool? InitialProcess { get; set; }

    }

    public class SearchTeacherProfileQueryHandler : IRequestHandler<SearchTeacherProfileQuery, IEnumerable<TeacherModel>>
    {
        private readonly AppDbContext context;
        private ICurrentUser User;

        private AppIdentityDbContext IDContext;

        public SearchTeacherProfileQueryHandler(AppDbContext context, ICurrentUser currentUser, AppIdentityDbContext idContext)
        {
            this.context = context;
            User = currentUser;
            IDContext = idContext;
        }
        public async Task<IEnumerable<TeacherModel>> Handle(SearchTeacherProfileQuery request, CancellationToken cancellationToken)
        {
            var query = context.Teachers.AsQueryable();

            if (request.ProcessID.HasValue)
            {
                if (request.InitialProcess.HasValue && request.InitialProcess.Value)
                {
                    //query = (from f in query
                    //         join q in context.ProfileProcesses on f.Id equals q.ID
                    //         where q.ApplicationID == null || q.ProcessID == request.ProcessID
                    //         select f);
                }
                else
                {
                    query = (from f in query
                             join q in context.ProfileProcesses on f.Id equals q.ID
                             where q.ProcessID == request.ProcessID
                             select f);
                }
            }
            if (!(await User.IsSuperAdmin()).Value)
            {
                var of = await User.GetOfficeID();
                query = query.Where(e => e.OfficeId == of);
            }
            if (!(await User.IsSuperAdmin()).Value)
            {
                var of = await User.GetOfficeID();

                var provincesUsers = IDContext.Users.Where(e => e.OfficeID == of).Select(e => e.Id).ToList();

                query = query.Where(e => provincesUsers.Contains((int)e.CreatedBy));

            }
            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }
            if(request.ServiceTypeId != null)
            {
                query = query.Where(e => e.SarviceTypId == request.ServiceTypeId);
            }
            if (!String.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                query = query.Where(e => EF.Functions.Like(e.Name, String.Concat("%", request.Name, "%")));
            }

            if (!String.IsNullOrEmpty(request.FatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.FatherName, String.Concat("%", request.FatherName, "%")));
            }

            return (await query.Select(p => new TeacherModel
            {

                Id = p.Id,
                Name = p.Name,
                LastName = p.LastName,
                FatherName = p.FatherName,
                FirstNameEng = p.FirstNameEng,
                LastNameEng = p.LastNameEng,
                FatherNameEng = p.FatherNameEng,
                Email = p.Email,
                GenderId = p.GenderId,
                SarviceTypId = p.SarviceTypId,
                ServiceText = p.SarviceTyp.NameDari,
                DateOfBirth = p.DateOfBirth,
                PhotoPath = p.PhotoPath,
                Code = p.Code,
                BirthLocationId = p.BirthLocationId,
                MaritalStatusId = p.MaritalStatusId,
                ReligionId = p.ReligionId,
                EthnicityId = p.EthnicityId,
                GenderText = p.Gender.Name,
                EthnicityText = p.Ethnicity.Name,
                Phone = p.Phone,
                DocumentTypeId = p.DocumentTypeId,
                DocumentTypeText = p.DocumentType.Name,
                NID = p.NationalId,
                 Province = p.Province,
                ProvinceText = p.ProvinceNavigation.Dari,
                District = p.District,
                DistrictText = p.DistrictNavigation.Dari,
                Salary = p.Salary,
                DobShamsi=PersianDate.GetFormatedString(p.DateOfBirth)
            }).Take(10).ToListAsync()).Select(cur =>
            {
                cur.NIDText = NationalIDReader.ConvertJSONToString(cur.NID, cur.DocumentTypeText);
                return cur;
            }).ToList();

        }
    }
}

