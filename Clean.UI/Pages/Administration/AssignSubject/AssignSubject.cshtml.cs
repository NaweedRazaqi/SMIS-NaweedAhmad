using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.SubjectAssignment.Models;
using App.Application.SubjectAssignment.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Administration.AssignSubject
{
    public class AssignSubjectModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfClassManagements = new List<SelectListItem>();
            var ClassManagement = await Mediator.Send(new GetClassManagementList());
            ClassManagement.ForEach(e => ListOfClassManagements.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfClassTypes = new List<SelectListItem>();
            var ClassType = await Mediator.Send(new GetClassTypeList());
            ClassType.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));

            ListOfYears = new List<SelectListItem>();
            var Year = await Mediator.Send(new GetYearList());
            Year.ForEach(e => ListOfYears.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
        }
        public async Task<IActionResult> OnPostSearch([FromBody] SearchSubjectAssignmentQuery query)
        {
            var result = new JsonResult(null);
            try
            {
                IEnumerable<SearchSubjectAssignmentModel> SaveResult = new List<SearchSubjectAssignmentModel>();

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
    }
}