using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Models;
using App.Application.Lookup.Queries;
using App.Application.Student.Relocation.Commands;
using App.Application.Student.Relocation.Models;
using App.Application.Student.Relocation.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Rellocation
{
    public class RellocationSubPageModel : BasePage
    {
        public async Task OnGetAsync([FromRoute] int id)
        {

            ListOfLocations = new List<SelectListItem>();

            var locations = await Mediator.Send(new GetLocationList() { ParentID = 1 });
            foreach (var location in locations)
                ListOfLocations.Add(new SelectListItem(location.Dari, location.Id.ToString()));
            ListOfSchoolTypes = new List<SelectListItem>();
            var schooltype = await Mediator.Send(new GetSchoolType());
            schooltype.ForEach(e => ListOfSchoolTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari }));

            ListOfSchools = new List<SelectListItem>();
            var schools = await Mediator.Send(new GetSchoolList());
            schools.ForEach(e => ListOfSchools.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));
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
        public async Task<IActionResult> OnPostSearch([FromBody] StudentRellocationSearchQuries query)
        {
            var result = new JsonResult(null);
            try
            {

                IEnumerable<SearchRellocationModel> SaveResult = new List<SearchRellocationModel>();

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

        public async Task<IActionResult> OnPostSave([FromBody] StudentRellocationCommand command)
        {
            try
            {
                IEnumerable<SearchRellocationModel> SaveResult = new List<SearchRellocationModel>();
                command.CreatedOn = DateTime.Now;
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

        public async Task<IActionResult> OnPostProvince([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var location = await Mediator.Send(new GetLocationList() { ParentID = Data.ID });
                foreach (var l in location)
                    SearchResult.Add(new { id = l.Id, text = l.Dari });

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

        public async Task<IActionResult> OnPostSchoolSearch([FromBody] GetSchoolLists query)
        {
            var result = new JsonResult(null);
            List<SearchSchoolModel> SaveResult = new List<SearchSchoolModel>();
            SaveResult = await Mediator.Send(query);
            var x = new JsonResult(SaveResult);
            return x;
        }
    }
}
