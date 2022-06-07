using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Examination.ScheduleExam.Commands;
using App.Application.Lookup.Queries;
using App.Application.Student.ScheduleExam.Models;
using App.Application.Student.ScheduleExam.Queries;
using Clean.Application.Documents.Queries;
using Clean.Application.System.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.ScheduleExam
{
    public class ScheduleExamModel : BasePage
    {
        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' data-id='$eid' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {
            #region LookUps
            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));
            ListOfClassManagementTypes = new List<SelectListItem>();
            var classmanagements = await Mediator.Send(new GetClassManagementList());
            classmanagements.ForEach(e => ListOfClassManagementTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            
            ListOfSchoolTypes = new List<SelectListItem>();
            var schooltype = await Mediator.Send(new GetSchoolType());
            schooltype.ForEach(e => ListOfSchoolTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari }));
            ListOfSubjectManagements = new List<SelectListItem>();
            var SubjectManagement = await Mediator.Send(new GetSubjectManagementList());
            SubjectManagement.ForEach(e => ListOfSubjectManagements.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
        
            #endregion

            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            ListOfDocumentTypes = new List<SelectListItem>();
            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID, Catagory = "ID" });
            foreach (var documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });

            try
            {
                var screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                string listout = "";
                foreach (var s in screens)
                {
                    listout = listout + htmlTemplate.Replace("$path", "dv_" + s.DirectoryPath.Replace("/", "_")).Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$eid", EncryptionHelper.Encrypt(s.Id.ToString())).Replace("$id", s.Id.ToString());
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }
        }
        public async Task<IActionResult> OnPostSubjectType([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var subjects = await Mediator.Send(new GetSubjectLists() { SchoolTypeID = Data.ID });
                foreach (var s in subjects)
                    SearchResult.Add(new { id = s.Id, text = s.Name });

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SearchResult },
                    Status = UIStatus.Success,
                    Text = "",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                result = new JsonResult(CustomMessages.FabricateException(ex));
            }
            return result;
        }
        public async Task<IActionResult> OnPostSearch([FromBody] SearchExamScheduleQueries query)
        {
            var result = new JsonResult(null);
            try
            {
                IEnumerable<SearchScheduleExamModel> SaveResult = new List<SearchScheduleExamModel>();

                SaveResult = await Mediator.Send(query);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                result.Value = new UIResult
                {
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace,
                    Data = null
                };
            }
            return result;
        }

        public async Task<IActionResult> OnPostSave([FromBody] CreateExamScheduleCommand command)
        {

            try
            {
                IEnumerable<SearchScheduleExamModel> SaveResult = new List<SearchScheduleExamModel>();
                command.CreatedOn = DateTime.Now;
               // command.ExamTime = Convert.ToDateTime(TimeZone.CurrentTimeZone);
                //command.DepartmentType = ProfileDepartmentType.General; 
                
                SaveResult = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "شهرت شاگرد موفقانه ثبت و راجستر گردید",
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
