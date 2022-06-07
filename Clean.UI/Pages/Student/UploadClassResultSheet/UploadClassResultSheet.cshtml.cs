using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Student.Examination.Commands;
using App.Application.Student.Examination.Models;
using App.Application.Student.Examination.Queries;
using App.Persistence.Models;
using Clean.Application.Documents.Queries;
using Clean.Application.System.Queries;
using Clean.Common;
using Clean.Common.Models;
using Clean.Common.Storage;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.UploadClassResultSheet
{
    public class ClassResultSheetModel : BasePage
    {

        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {
          

        ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));
            ListOfSchoolTypes = new List<SelectListItem>();
            var schooltype = await Mediator.Send(new GetSchoolType());
            schooltype.ForEach(e => ListOfSchoolTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari }));
            ListOfClassManagementTypes = new List<SelectListItem>();
            var classmanagements = await Mediator.Send(new GetClassManagementList());
            classmanagements.ForEach(e => ListOfClassManagementTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            ListOfSchools = new List<SelectListItem>();
            var schools = await Mediator.Send(new GetSchoolList());
            schools.ForEach(e => ListOfSchools.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));
            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);
            ListOfDocumentTypes = new List<SelectListItem>();
            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID,Catagory = "SH" });
            foreach (var documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });

            try
            {
                var screens = await Mediator.Send(new GetSubScreens() { ID = ScreenID });
                string listout = "";
                foreach (var s in screens)
                {
                    listout = listout + htmlTemplate.Replace("$path", "dv_" + s.DirectoryPath.Replace("/", "_")).Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$id", s.Id.ToString());
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }

        }

        public async Task<IActionResult> OnPostUpload([FromForm] IFormFile attachement)
        {
            if (attachement.ContentType.Contains("image") || attachement.ContentType.EndsWith("pdf"))
            {
                var addition = DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                var root = AppConfig.DocumentsPath + addition;
                FileStorage _storage = new FileStorage();

                var extension = System.IO.Path.GetExtension(attachement.FileName);
                string filename = await _storage.CreateAsync(attachement.OpenReadStream(), extension, root);
                var result = new
                {
                    Status = 1,
                    Text = "فایل موفقانه ارسال گردید",
                    Description = "لطفاً فورم را درج نموده و ارسال بدارید",
                    url = addition + filename
                };
                return new JsonResult(result);
            }
            else
            {
                var result = new
                {
                    Status = 0,
                    Text = "فارمت فایل درست نیست",
                    Description = "لطفاً فایل تان را به عکس یا به PDF تبدیل نموده ضمیمه بسازید."
                };
                return new JsonResult(result);
            }
        }
        public async Task<IActionResult> OnPostSave([FromBody] CreateStudentClassResultSheetCommand command)
        {
            try
            {
                IEnumerable<StudentClassResultSheetModel> SaveResult = new List<StudentClassResultSheetModel>();
                SaveResult = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "مشخصات استاد موفقانه ثبت و راجستر گردید",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSchoolType([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var school = await Mediator.Send(new GetSchoolLists() { SchoolTypeID = Data.ID });
                foreach (var s in school)
                    SearchResult.Add(new { id = s.Id, text = s.Dari });

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
     
        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentClassResultSheetQuery query)
        {
            var result = new JsonResult(null);
            try
            {
                IEnumerable<StudentClassResultSheetModel> SaveResult = new List<StudentClassResultSheetModel>();

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

        public async Task<IActionResult> OnPostDownload([FromBody] UploadedFile file)
        {
            FileStorage _storage = new FileStorage();
            var filepath = AppConfig.DocumentsPath + file.Name;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file.Name);
        }
    }
}
