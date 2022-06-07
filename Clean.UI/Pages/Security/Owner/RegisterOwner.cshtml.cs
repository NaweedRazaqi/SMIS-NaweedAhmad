using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Lookup.Queries;
using Clean.Application.Lookup.Queries;
using Clean.Application.System.Commands;
using Clean.Application.System.Queries;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security.Owner
{
    public class RegisterOwnerModel : BasePage
    {

        public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {

            ListOfOwnersParent = new List<SelectListItem>();
            var OwnersParent = await Mediator.Send(new GetOwnerParentList());
            OwnersParent.ForEach(O => ListOfOwnersParent.Add(new SelectListItem { Value = O.Id.ToString(), Text = O.Name }));

            ListOfProvinces = new List<SelectListItem>();
            var ProvinceList = await Mediator.Send(new GetOnlyProvinceList());
            ProvinceList.ForEach(f => ListOfProvinces.Add(new SelectListItem { Value = f.ID.ToString(), Text = f.Title }));

        }

        public async Task<IActionResult> OnPostSave([FromBody] CreateOwnerCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "موقف داده شده موفقانه ثبت گردید.",
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSearch([FromBody] SeachOwnerQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = string.Empty,
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

