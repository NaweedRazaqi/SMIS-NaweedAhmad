using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Prf.Commands;
using App.Application.Prf.Models;
using App.Application.Prf.Queries;
using App.Application.Student.Prf.Commands;
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Profile
{
    public class StudentParentDetails : BasePage
    {
        public async Task OnGetAsync([FromRoute] int id)
        {

            ListOfRelatives = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetStudentRelativeType());
            classes.ForEach(e => ListOfRelatives.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari }));

            ListOfLocations = new List<SelectListItem>();
            var locations = await Mediator.Send(new GetLocationList() { ParentID = 1 });
            foreach (var location in locations)
                ListOfLocations.Add(new SelectListItem(location.Dari, location.Id.ToString()));

            ListOfProfession = new List<SelectListItem>();
            var professions = await Mediator.Send(new GetProfession());
            professions.ForEach(e => ListOfProfession.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Dari }));
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentParentsDetails  query)
        {
            var result = new JsonResult(null);
            try
            {

                IEnumerable<StudentParentDetailsModel> SaveResult = new List<StudentParentDetailsModel>();

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

        public async Task<IActionResult> OnPostSave([FromBody] AddStudentParentsDetailsCommand command)
        {
            try
            {
                IEnumerable<StudentParentDetailsModel> SaveResult = new List<StudentParentDetailsModel>();
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
    }
}
