using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Student.Examination.Result.Models;
using App.Application.Student.Relocation.Queries;
using App.Application.Student.Sawaneh.Models;
using App.Application.Student.Sawaneh.Queries;
using Clean.Application.Documents.Queries;
using Clean.Application.System.Queries;
using Clean.Common;
using Clean.Common.Enums;
using Clean.Common.Storage;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Profile.Sawaneh
{
    public class StudentSearchModel : BasePage
    {

        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' data-id='$eid' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {
            ListOfGenders = new List<SelectListItem>();
            var genders = await Mediator.Send(new GetGenderList());
            genders.ForEach(e => ListOfGenders.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Name }));

            ListOfMaritalStatus = new List<SelectListItem>();
            var maritals = await Mediator.Send(new GetMaritalList());
            maritals.ForEach(e => ListOfMaritalStatus.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfEthnicities = new List<SelectListItem>();
            var ethnicities = await Mediator.Send(new GetEthnicityList() { ParentID = 1 });
            foreach (var ethnicity in ethnicities)
                ListOfEthnicities.Add(new SelectListItem(ethnicity.Name, ethnicity.Id.ToString()));

            ListOfReligions = new List<SelectListItem>();
            var religions = await Mediator.Send(new GetReligionList() { ParentID = 1 });
            foreach (var religion in religions)
                ListOfReligions.Add(new SelectListItem(religion.Name, religion.Id.ToString()));


            ListOfLocations = new List<SelectListItem>();

            var locations = await Mediator.Send(new GetLocationList() { ParentID = 1 });
            foreach (var location in locations)
                ListOfLocations.Add(new SelectListItem(location.Dari, location.Id.ToString()));


            ListOfSchools = new List<SelectListItem>();
            var schools = await Mediator.Send(new GetSchoolList());
            schools.ForEach(e => ListOfSchools.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));


            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));




            ListOfLanguages = new List<SelectListItem>();
            var languages = await Mediator.Send(new GetLanguagesList());
            languages.ForEach(e => ListOfLanguages.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            // get list of subscreens
            string Screen = EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]);
            int ScreenID = Convert.ToInt32(Screen);

            ListOfDocumentTypes = new List<SelectListItem>();
            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID });
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

        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentSawanehQuery query)
        {
            var result = new JsonResult(null);
            try
            {
                IEnumerable<SawanehSearchModel> SaveResult = new List<SawanehSearchModel>();

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


        public async Task<IActionResult> OnGetDownload([FromQuery] string file, [FromQuery] string uploadType)
        {
            FileStorage _storage = new FileStorage();
            var basePath = "";
            if (uploadType == UploadTypes.Photo)
            {
                basePath = AppConfig.ImagesPath;
            }
            else if (uploadType == UploadTypes.Signature)
            {
                basePath = AppConfig.SignaturesPath;
            }
            var filepath = basePath + file;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file);
        }
    }
}
