using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Prf.Commands;
using App.Application.Prf.Models;
using App.Application.Prf.Queries;
using App.Application.Student.Prf.Commands;
using App.Application.Student.Prf.Models;
using App.Application.Student.Prf.Queries;
using Clean.UI.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clean.UI.Pages.Student.Profile
{
    public class StudentHealthStatusModel : BasePage
    {
        public void OnGet([FromRoute] int id)
        {




        }

        public async Task<IActionResult> OnPostSave([FromBody] StudentHealthReportCommand command)
        {
            try
            {
                IEnumerable<StudentHealthReportModel> SaveResult = new List<StudentHealthReportModel>();
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentHealthReportQuery query)
        {
            var result = new JsonResult(null);
            try
            {

                IEnumerable<StudentHealthReportModel> SaveResult = new List<StudentHealthReportModel>();

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
