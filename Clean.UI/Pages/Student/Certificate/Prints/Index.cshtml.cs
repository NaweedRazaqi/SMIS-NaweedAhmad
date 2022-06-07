using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Student.Certificate.Queries;
using App.Application.Student.Prf.Queries;
using App.Application.Student.ResultSheet.Queries;
using Clean.Common;
using Clean.Common.Enums;
using Clean.Common.Storage;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Clean.UI.Pages.Student.Certificate.Prints
{
    public class IndexModel : BasePage
    {

        public string FirstName { get; set; }
        public string FirstNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public string FatherName { get; set; }
        public string StudentSchool { get; set; }
        public string BirthLocationName { get; set; }
        public string StudentClass { get; set; }
        public long? ProfileId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public string YearOfBirth { get; set; }
        public string SchoolNameEng { get; set; }
        public string Code { get; set; }
        public string ProfilePhoto { get; set; }
        public int subjectnumber { get; set; }
        public long? total10thmark { get; set; }
        public long? total11thmark { get; set; }
        public long? total12thmark { get; set; }
        public long? GrandTotalMarks { get; set; }
        public long? Average { get; set; }
     
        public List<SubjectResult> SubjectResults = new List<SubjectResult>();
        private readonly IConfiguration Config;
        public IndexModel(IConfiguration configuration, ICurrentUser currentUser)
        {
            Config = configuration;
        }

        public async Task OnGet([FromQuery] long recordId)
        {
            // Fetching Student Profile
            var result = await this.Mediator.Send(new SearchProfileQuery { ID = recordId });
            var cur = result.FirstOrDefault();
            if (!result.Any())
            {
                FirstName = "موجود نیست";
                FatherName = "موجود نیست";
                BirthLocationName = "موجود نیست";
                YearOfBirth = "موجود نیست";
                FirstNameEng = "موجود نیست";
                FatherNameEng = "موجود نیست";
                Code = "موجود نیست";
                ProfilePhoto = "موجود نیست";
            }
            else
            {
                FirstName = cur.FirstName;
                FatherName = cur.FatherName;
                BirthLocationName = cur.BirthLocationName;
                YearOfBirth = cur.YearOfBirth;
                FirstNameEng = cur.FirstNameEng;
                FatherNameEng = cur.FatherNameEng;
                Code = cur.Code;
                ProfilePhoto = await GetFile("ProfilePhotos", cur.PhotoPath);
            }
            var result2 = await this.Mediator.Send(new SearchResultSheetQuery { ID = recordId });
            var cur2 = result2.FirstOrDefault();
            if (!result2.Any())
            {
                StudentSchool = "موجود نیست";
                StudentAssassNumber = 000;
                SchoolNameEng = "موجود نیست";
            }
            else {
                StudentSchool = cur2.StudentSchoolName;
                StudentAssassNumber = cur2.StudentAssassNumber;
                SchoolNameEng = cur2.SchoolNameEng;
            }
            // Fetching Student 3 years marks 
            var Result3 = await this.Mediator.Send(new SearchStudentTranscriptQuery { ProfileId = recordId });
            var cur3 = Result3.FirstOrDefault();

            if (!Result3.Any())
            {
                total10thmark = 0;
                total11thmark = 0;
                total12thmark = 0;
                GrandTotalMarks = 0;
                Average = 0;
            }
            else
            {


                total10thmark = 0;
                total11thmark = 0;
                total12thmark = 0;
                GrandTotalMarks = 0;
                Average = 0;
                foreach (var item in Result3)
                {

                    var curItem = SubjectResults.Where(e => e.Subject == item.StudentSubjects).SingleOrDefault();
                    if (curItem == null)
                    {
                        curItem = new SubjectResult
                        {
                            Subject = item.StudentSubjects,

                        };
                        SubjectResults.Add(curItem);
                    }
                    curItem.class10thmarks = item._10classMark.Value;

                    curItem.clas11thmarks = item._11classMark.Value;
                    curItem.class12thmarks = item._12classMark.Value;

                    total10thmark += curItem.class10thmarks;
                    total11thmark += curItem.clas11thmarks;
                    total12thmark += curItem.class12thmarks;
                }
                GrandTotalMarks = total10thmark + total11thmark + total12thmark;


                Average = GrandTotalMarks / SubjectResults.Count();
            }
            // Printing number for  subjects in transcipt
            var result4 = await this.Mediator.Send(new SearchStudentTranscriptQuery { ProfileId = recordId });
            var cur4 = result4.FirstOrDefault();
            int number;
            number = 0;
            while(number <= SubjectResults.Count())
            {

                number = number + 1;
            }
            subjectnumber = number;
            

            //if (SubjectResults != null)
            //{

            //    int number = 1;
            //    while (number <= SubjectResults.Count())
            //    {
            //        ++number;

            //    }
            //    subjectNumber = number;
            //}
           
        }


        public async Task<string> GetFile(String Dir, String FileName)
        {
            FileStorage _storage = new FileStorage();
            var filepath = Config[Dir] + FileName;
            var dirpath = AppConfig.ImagesPath;
            var fullpath = dirpath + filepath;
            System.IO.Stream filecontent = await _storage.GetAsync(fullpath);

            byte[] filebytes = new byte[filecontent.Length];
            filecontent.Read(filebytes, 0, Convert.ToInt32(filecontent.Length));
            String Result = "data:" + _storage.GetContentType(filepath) + ";base64," + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            return Result;
        }

        public class SubjectResult
        {
            public string Subject { get; set; }
            public long clas11thmarks { get; set; }
            public long class10thmarks { get; set; }
            public long class12thmarks { get; set; }
            public int subjectnumber { get; set; }  
          
        }
       

    }
}
