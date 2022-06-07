using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Student.ResultSheet.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Student.ResultSheet.Prints
{
    public class IndexModel : BasePage
    {

        public long? MarksId { get; set; }
        public long? Marks { get; set; }
        public string StudentSubjects { get; set; }
        public int? StudentExamType { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string StudentSchool { get; set; }
        public string StudentClass { get; set; }
        public long? ProfileId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public long? TotalMidTermMark { get; set; }
        public long? TotalFinalTermMark { get; set; }
        public long? TotalMark { get; set; }
        public long? MidTermAverage { get; set; }
        public long? FinalTermAverage { get; set; }
        public long? Average { get; set; }
        public string MidTermResult { get; set; }
        public string FinalTermResult { get; set; }
        public string FinalResult { get; set; }
        public string Code { get; set; }
        public List<SubjectResult> SubjectResults = new List<SubjectResult>();
        
        public async Task OnGet([FromQuery] long recordId)
        {

            var result = await this.Mediator.Send(new SearchStudentSubjectMarksQuery { ProfileId = recordId });
            var cur = result.FirstOrDefault();

            if (!result.Any())
            {
                FirstName = "معلومات موجود نیست";
                FatherName = "معلومات موجود نیست";
                StudentClass = "معلومات موجود نیست";
                StudentSchool = "معلومات موجود نیست";
                FirstName = "معلومات موجود نیست";
                FirstName = "معلومات موجود نیست";
                FirstName = "معلومات موجود نیست";
                TotalMidTermMark = 0;
                TotalFinalTermMark = 0;
                TotalMark = 0;
                MidTermAverage = 0;
                FinalTermAverage = 0;
                Average = 0;
            }

            if (result.Any())
            {
                FirstName = cur.FirstName;
                FatherName = cur.FatherName;
                StudentClass = cur.StudentClass;
                StudentSchool = cur.StudentSchool;

                TotalMidTermMark = 0;
                TotalFinalTermMark = 0;
                TotalMark = 0;
                MidTermAverage = 0;
                FinalTermAverage = 0;
                Average = 0;

                foreach (var item in result)
                {
                    //var counter;
                    var curItem = SubjectResults.Where(e => e.Subject == item.StudentSubjects).SingleOrDefault();
                    if (curItem == null)
                    {
                        curItem = new SubjectResult
                        {
                            Subject = item.StudentSubjects,

                        };
                        SubjectResults.Add(curItem);
                    }
                    if (item.StudentExamType.Value == 1)
                    {
                        curItem.MidMark = item.Marks.Value;
                        TotalMidTermMark += item.Marks.Value;
                        MidTermAverage = TotalMidTermMark / SubjectResults.Count();

                        if (MidTermAverage < 50)
                        {
                            MidTermResult = "ناکام";

                        }
                        else
                        {
                            MidTermResult = "کامیاب";
                        }
                    }
                    else if (item.StudentExamType.Value == 2)
                    {
                        curItem.FinalMark = item.Marks.Value;
                        TotalFinalTermMark += item.Marks.Value;
                        FinalTermAverage = TotalFinalTermMark / SubjectResults.Count();

                        if (FinalTermAverage < 50)
                        {
                            FinalTermResult = "ناکام";

                        }
                        else
                        {
                            FinalTermResult = "کامیاب";
                        }

                    }
                }

                TotalMark = TotalMidTermMark + TotalFinalTermMark;
                Average = TotalMark / SubjectResults.Count();
                if (Average < 50)
                {
                    FinalResult = "ناکام";

                }
                else
                {
                    FinalResult = "کامیاب";
                }

              
            }
            //Results.Add(cur.StudentSubjects);
            //Results.Add((cur.Marks).ToString());
            var result2 = await this.Mediator.Send(new SearchResultSheetQuery { ID = recordId });
            var cur2 = result2.FirstOrDefault();
            if (!result2.Any())
            {
                StudentAssassNumber = 0;
                Code = "معلومات موجود نیست";
            }
            if (result2.Any())
            {
                StudentAssassNumber = cur2.StudentAssassNumber;
                Code = cur2.Code;
            }

            
        }

    }

    public class SubjectResult
    {
        public string Subject { get; set; }
        public long? MidMark { get; set; }
        public long FinalMark { get; set; }
        
    }
}
