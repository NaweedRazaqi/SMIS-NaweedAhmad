using System;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Student.Prf.Queries;
using App.Application.Student.Registration.Queries;
using App.Application.Student.Relocation.Queries;
using Clean.Common;
using Clean.Common.Storage;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Clean.UI.Pages.Student.Rellocation.Print
{
    public class IndexModel : BasePage
    {
        private IConfiguration Config { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string PhotoPath { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string ProvinceText { get; set; }
        public string District { get; set; }
        public string DistrictText { get; set; }
        public string ClassTypeName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolId { get; set; }
        public string EducationTypeName { get; set; }
        public int? StudentAssasNumber { get; set; }
        public string ParentPositionName { get; set; }
        public string FatherJobLocation { get; set; }
        public string BirthLocationName { get; set; }
        public string GranthFatherPositionName { get; set; }
        public string GrantherFatherJobLocation { get; set; }
        public string GrandFatherLocation { get; set; }
        public string NewSchoolNameText { get; set; }
        public string OldSchoolNameText { get; set; }
        public string SchoolLocationNameText { get; set; }
        public string DistrictName { get; set; }
        public string Gurrentername { get; set; }
        public string GurrenterJob { get; set; }
        public string GurrenterAddress { get; set; }
        public string GurrenterFatherName { get; set; }

        public string StudentPhoto { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            this.Config = configuration;
        }
        public async Task OnGet([FromQuery] long recordId)
        {
            var result = await this.Mediator.Send(new SearchProfileQuery { ID = recordId });
            var cur = result.FirstOrDefault();
            if (!result.Any())
            {
                FirstName = "معلومات موجود نیست";
                FatherName = "معلومات موجود نیست";
                GrandFatherName = "معلومات موجود نیست";
                BirthLocationName = "معلومات موجود نیست";
                GrandFatherLocation = "معلومات موجود نیست";
                
            }
            else {
            FirstName = cur.FirstName;
            FatherName = cur.FatherName;
            GrandFatherName = cur.GrandFatherName;
            BirthLocationName = cur.BirthLocationName;
            GrandFatherLocation = cur.BirthLocationName;

            StudentPhoto = await GetFile("ImagesPath", cur.PhotoPath);
            }

            var StudentRegisteration = await this.Mediator.Send(new SearchStudentRegistrationQueries { ProfileId = recordId});
            var STR = StudentRegisteration.FirstOrDefault();
            if (!StudentRegisteration.Any()) {
            
                SchoolName = "معلومات موجود نیست";
                ClassTypeName = "معلومات موجود نیست";
                StudentAssasNumber = 0;

            }
            else { 
            SchoolName = STR.SchoolName;
            ClassTypeName = STR.ClassTypeText;
            StudentAssasNumber = STR.StudentAssassNumber;
            }
            var result2 = await this.Mediator.Send(new SearchStudentParentsDetails { ProfileId = recordId});

            if (!result2.Any())
            {
                ParentPositionName = "معلومات موجود نیست";
                FatherJobLocation = "معلومات موجود نیست";
                GranthFatherPositionName = "معلومات موجود نیست";
                GrantherFatherJobLocation = "معلومات موجود نیست";
                Gurrentername = "معلومات موجود نیست";
                GurrenterJob = "معلومات موجود نیست";
                GurrenterAddress = "معلومات موجود نیست";
                GurrenterFatherName = "معلومات موجود نیست";
            }
            else { 
             result2.ToList().ForEach(cur2=>
            {
                if (cur2.RelativeTypeId == 5)
                {

                    ParentPositionName = cur2.ProfesionName;
                    FatherJobLocation = cur2.JobLocationName;


                }
                else if (cur2.RelativeTypeId == 7)
                {
                    GranthFatherPositionName = cur2.ProfesionName;
                    GrantherFatherJobLocation = cur2.JobLocationName;

                }

                Gurrentername = cur2.GurrenterName;
                GurrenterJob = cur2.ProfesionName;
                GurrenterAddress = cur2.JobLocationName;
                GurrenterFatherName = cur2.GurrenterFatherName;
            });
            }
            var result3 = await this.Mediator.Send(new StudentRellocationSearchQuries { ProfileId = recordId });
            var cur3 = result3.FirstOrDefault();

            if (!result3.Any())
            {
                NewSchoolNameText = "معلومات موجود نیست";
                SchoolLocationNameText = "معلومات موجود نیست";
                DistrictName = "معلومات موجود نیست";
            }
            else { 
            NewSchoolNameText = cur3.NewSchoolNameText;
            SchoolLocationNameText = cur3.SchoolLocationNameText;
            DistrictName = cur3.DistrictName;
            }
        }

        public async Task<string> GetFile(String Dir, String FileName)
        {
            FileStorage _storage = new FileStorage();
            var filepath = Config[Dir] + FileName;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);

            byte[] filebytes = new byte[filecontent.Length];
            filecontent.Read(filebytes, 0, Convert.ToInt32(filecontent.Length));
            String Result = "data:" + _storage.GetContentType(filepath) + ";base64," + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            return Result;
        }
        public async Task<IActionResult> OnPostDownload([FromBody] Common.Storage.UploadedFile file)
        {
            Common.Storage.FileStorage _storage = new Common.Storage.FileStorage();
            var filepath = AppConfig.SystemPhotosPath + file.Name;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file.Name);
        }
    }
}
