using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Accounts.Commands;
using Clean.Application.Accounts.Models;
using Clean.Application.Accounts.Queries;
using Clean.Application.Lookup.Queries;
using Clean.UI.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security
{
    public class ModuleActivationModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfModuleStatus = new List<SelectListItem>();
            var ModuleStatus = await Mediator.Send(new GetModuleStausList());
            ModuleStatus.ForEach(e => ListOfModuleStatus.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.IsActiveName }));

        }

        public async Task<IActionResult> OnPostSearch([FromBody] GetActivationQuery query)
        {
            var result = new JsonResult(null);
            try
            {

                IEnumerable<ModuleActivation> SaveResult = new List<ModuleActivation>();

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



        public async Task<IActionResult> OnPostSave([FromBody] SaveModuleStatusCommand command)
        {
            try
            {
                IEnumerable<ModuleActivation> SaveResult = new List<ModuleActivation>();
              
                // command.ServiceTypeID = ServiceType.Student;
                SaveResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "حالت بخش موفقانه تبدیل گردید.",
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
