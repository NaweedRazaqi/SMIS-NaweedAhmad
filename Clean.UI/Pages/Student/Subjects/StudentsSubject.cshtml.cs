using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Student.Examination.Commands;
using App.Application.Student.Examination.Models;
using App.Application.Student.Examination.SearchStudentSchool;
using Clean.Application.System.Queries;
using Clean.Common.Exceptions;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Subjects
{
    public class StudentSubjectModel : BasePage
    {
        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen' data-id='$data'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {
            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));

            ListOfClassManagementTypes = new List<SelectListItem>();
            var classmanagements = await Mediator.Send(new GetClassManagementList());
            classmanagements.ForEach(e => ListOfClassManagementTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            ListOfSchools = new List<SelectListItem>();
            var schools = await Mediator.Send(new GetSchoolList());
            schools.ForEach(e => ListOfSchools.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));

            listOfExamType = new List<SelectListItem>();
            var exams = await Mediator.Send(new GetExamTypeList());
            exams.ForEach(e => listOfExamType.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));

            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            try
            {
                var screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                string listout = "";
                foreach (var s in screens)
                {
                    listout = listout +
                        htmlTemplate.Replace("$path", "dv_" + s.DirectoryPath.Replace("/", "_"))
                        .Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$id", s.Id.ToString())
                        .Replace("$data", EncryptionHelper.Encrypt(s.Id.ToString()));
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }
        }        
        public async Task<ActionResult> OnPostSearch([FromBody] SearchStudentSchoolQuery request)
        {

          
                var result = new JsonResult(null);
                IEnumerable<SearchStudentSchoolProfileModel> SaveResult = new List<SearchStudentSchoolProfileModel>();
                SaveResult = await Mediator.Send(request);
                var x = new JsonResult(SaveResult);
                return x;
          
        }

        public async Task<IActionResult> OnPostSave([FromBody] SaveStudentClassMarksCommand command)
        {
            try
            {
                IEnumerable<SchoolStudentClassMarksModel> SaveResult = new List<SchoolStudentClassMarksModel>();
                command.CreatedOn = DateTime.Now;
                SaveResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "نمرات صنف موافقانه ثبت گردید!.",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }

      

    }
}

