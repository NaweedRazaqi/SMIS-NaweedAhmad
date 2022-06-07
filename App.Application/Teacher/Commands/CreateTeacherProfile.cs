using App.Application.Prf.Models;
using App.Application.Prf.Queries;
using App.Application.Prf.Queries.SearchTeacher;
using App.Application.Teacher.Models;
using App.Persistence.Context;
using Clean.Common.Exceptions;
using Clean.Common.Extensions;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace App.Application.Prf.Commands.CreateTeacher
{
    public class CreateTeacherProfile : IRequest<List<TeacherModel>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CreatedBy { get; set; }
        public int? GenderId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Salary { get; set; }
        public int? SarviceTypId { get; set; }
        public string LastName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public string NationalId { get; set; }
        public int? EthnicityId { get; set; }
        public string PhotoPath { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? OfficeId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Code { get; set; }
        public string NID { get; set; }
        public int? StatusID { get; set; }
    }



    public class CreateTeacherProfileHandler : IRequestHandler<CreateTeacherProfile, List<TeacherModel>>
    {

        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;
        public CreateTeacherProfileHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;

        }
        public async Task<List<TeacherModel>> Handle(CreateTeacherProfile request, CancellationToken cancellationToken)
        {

            bool em = IsValidEmailAddress(request.Email);
            if (!em)
            {
                throw new BusinessRulesException("ایمیل آدرس درست نیست");
            }

            bool mo = IsPhoneNumber(request.Phone.ToString());

            if (!mo)
            {
                throw new BusinessRulesException("شماره  درست نیست! شماره با 07 شروع.باید 10 عدد باشد");
            }
            IEnumerable<TeacherModel> result = new List<TeacherModel>();
            var profile = request.Id.HasValue ? context.Teachers.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Teacher();
            int CurrentUserId = await currentUser.GetUserId();

            profile.Name = request.Name.Trim();
            profile.LastName = request.LastName;
            profile.FatherName = request.FatherName;
            profile.FirstNameEng = request.FirstNameEng;
            profile.LastNameEng = request.LastNameEng;
            profile.FatherNameEng = request.FatherNameEng;
            profile.DateOfBirth = request.DateOfBirth.Value;
            profile.Email = request.Email;
            profile.BirthLocationId = request.BirthLocationId.Value;
            profile.GenderId = request.GenderId.Value;
            profile.MaritalStatusId = request.MaritalStatusId.Value;
            profile.EthnicityId = request.EthnicityId.Value;
            profile.ReligionId = request.ReligionId;
            profile.NationalId = request.NID;
            profile.PhotoPath = request.PhotoPath;
            profile.DocumentTypeId = request.DocumentTypeId.Value;
            profile.Province = request.Province;
            profile.District = request.District;
            profile.Phone = request.Phone;
            profile.SarviceTypId = request.SarviceTypId;
            profile.PhotoPath = request.PhotoPath;
            profile.Salary = request.Salary;

            if (!request.Id.HasValue)
            {
                #region BuildHrCode
                StringBuilder PrefixBuilder = new StringBuilder(string.Empty);
                StringBuilder HrCodeBuilder = new StringBuilder(string.Empty);

                // Build Prefix
                PrefixBuilder.Append("TS");
                PrefixBuilder.Append(("00" + request.BirthLocationId.ToString()).Right(2));
                PrefixBuilder.Append(("00" + Convert.ToDateTime(request.DateOfBirth).Year.ToString()).Right(2));
                PrefixBuilder.Append(("00" + Convert.ToDateTime(request.DateOfBirth).Month.ToString()).Right(2));

                //Build Suffix
                //Get Current Suffix where its prefix is equal to PrefixBuilder.
                //int? Suffix;
                //var last = await context.Teachers.Where(p => p.Prefix == PrefixBuilder.ToString()).OrderByDescending(e => e.Suffix).FirstOrDefaultAsync();
                //int? CurrentSuffix = last == null ? 0 : last.Suffix;
                //if (CurrentSuffix is null) CurrentSuffix = 0;
                //Suffix = CurrentSuffix + 1;

                // Build HR Code
                HrCodeBuilder.Append(PrefixBuilder.ToString());
                //HrCodeBuilder.Append(("000" + Suffix.ToString()).Right(3));
                #endregion BuildHrCode

                profile.Code = HrCodeBuilder.ToString();
                profile.ModifiedBy = "";
                profile.ModifiedOn = DateTime.Now;
                profile.CreatedBy = CurrentUserId;
                profile.CreatedOn = DateTime.Now;
                profile.OfficeId = (await currentUser.GetOfficeID()).Value;
                // profile.ServiceTypeId = request.ServiceTypeID.Value;

                context.Teachers.Add(profile);
            }
            else
            {
                profile.StatusId = request.StatusID.HasValue ? request.StatusID.Value : 1;
                profile.ModifiedBy += "," + CurrentUserId;
                profile.ModifiedOn = DateTime.Now;
            }


            await context.SaveChangesAsync();
            result = await mediator.Send(new SearchTeacherProfileQuery() { Id = profile.Id });

            return result.ToList();

        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            bool MethodResult = false;

            try
            {
                MailAddress m = new MailAddress(emailAddress);

                MethodResult = m.Address == emailAddress;

            }
            catch //(Exception ex)
            {
                //ex.HandleException();

            }

            return MethodResult;

        }

        public static bool IsPhoneNumber(string number)
        {

            return number[0] == '0' && number[1] == '7' && number.Length == 10 && IsDigit(number);

        }
        static bool IsDigit(string Input)
        {
            foreach (char c in Input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
