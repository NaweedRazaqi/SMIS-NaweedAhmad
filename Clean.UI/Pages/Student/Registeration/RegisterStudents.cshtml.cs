using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Application.Student.Registration.Commands;
using App.Application.Student.Registration.Model;
using App.Application.Student.Registration.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Registeration
{
    public class RegisterStudentsModel : BasePage
    {
        public async Task OnGetAsync([FromRoute] int id)
        {
            #region LookUps
            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));
            ListOfClassManagementTypes = new List<SelectListItem>();
            var classmanagements = await Mediator.Send(new GetClassManagementList());
            classmanagements.ForEach(e => ListOfClassManagementTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            ListOfSchoolTypes = new List<SelectListItem>();
            var schooltype = await Mediator.Send(new GetSchoolType() ) ;
            schooltype.ForEach(e => ListOfSchoolTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari}));
            ListOfSchoolCategory = new List<SelectListItem>();
            var schoolCategory = await Mediator.Send(new GetSchoolCategoryList());
            schoolCategory.ForEach(e => ListOfSchoolCategory.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            ListOfProvinces = new List<SelectListItem>();
            var provincelist = await Mediator.Send(new GetOnlyProvinceList());
            provincelist.ForEach(e => ListOfProvinces.Add(new SelectListItem { Value = e.ID.ToString(), Text = e.Title }));
            ListOfDistricts = new List<SelectListItem>();
            var districtlist = await Mediator.Send(new GetOnlyDistricts());
            districtlist.ForEach(e => ListOfDistricts.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            #endregion

        }

        #region SchoolType
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
        public async Task<IActionResult> OnPostProvinceType([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var province = await Mediator.Send(new GetOnlyDistricts() { ProvinceID = Data.ID });
                foreach (var p in province)
                    SearchResult.Add(new { id = p.Id, text = p.Name });

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
        public async Task<IActionResult> OnPostSchoolCategory([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var schoolcategory = await Mediator.Send(new GetSchoolLists() {  SchoolCategoryId= (short?)Data.ID });
                foreach (var s in schoolcategory)
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
        #endregion
        public async Task<IActionResult> OnPostSave([FromBody] AddStudentRegisterationCommand command)
        {
            try
            {
                IEnumerable<StudentRegisterationModel> SaveResult = new List<StudentRegisterationModel>();
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentRegistrationQueries query)
        {
            var result = new JsonResult(null);
            try
            {
                IEnumerable<StudentRegisterationModel> SaveResult = new List<StudentRegisterationModel>();
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
        public async Task<IActionResult> OnPostSchoolCategorySearch([FromBody] GetSchoolLists query)
        {
            var result = new JsonResult(null);
            List<SearchSchoolModel> SaveResult = new List<SearchSchoolModel>();
            SaveResult = await Mediator.Send(query);
            var x = new JsonResult(SaveResult);
            return x;
        }
    }
}
