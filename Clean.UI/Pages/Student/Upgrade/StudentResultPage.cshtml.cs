using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using App.Application.Student.Examination.Commands;
using App.Application.Student.Examination.Models;
using App.Application.Student.Examination.Result.Models;
using App.Application.Student.Examination.Result.Queries;
using App.Application.Student.Examination.StudentUpgrade.Commands;
using App.Application.Student.Examination.StudentUpgrade.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Upgrade
{
    public class StudentResultPageModel:BasePage
    {
        public async Task OnGetAsync()
        {

            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));
            ListOfClassManagementTypes = new List<SelectListItem>();
            var classmanagements = await Mediator.Send(new GetClassManagementList());
            classmanagements.ForEach(e => ListOfClassManagementTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));
            ListOfResults = new List<SelectListItem>();
            var results = await Mediator.Send(new GetResultList());
            results.ForEach(e => ListOfResults.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));

        }



        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentResultQuery query)
        {
            var result = new JsonResult(null);
            try
            { 
                IEnumerable<SearchStudentNewResultModel> SaveResult = new List<SearchStudentNewResultModel>();

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

        public async Task<IActionResult> OnPostSave([FromBody] CreateStudentUpgradeCommand command)
        {
            try
            {
                IEnumerable<StudentUpgradeSearch> SaveResult = new List<StudentUpgradeSearch>();
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
