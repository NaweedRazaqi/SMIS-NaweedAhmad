using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Lookup.Queries;
using Clean.Application.System.Commands;
using Clean.Application.System.Queries;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Security.Owner
{
    public class AssignUserOwnerModel : BasePage
    {

         public string SubScreens { get; set; }
        private string htmlTemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>";
        public async Task OnGetAsync()
        {

            ListOfUsers = new List<SelectListItem>();
            var userslist = await Mediator.Send(new GetUserNameList());
            userslist.ForEach(u => ListOfUsers.Add(new SelectListItem { Value = u.Id.ToString(), Text = u.UserName + " "+ u.LastName }));
            ListOfOwners = new List<SelectListItem>();
            var OwnersParent = await Mediator.Send(new GetOwnerParentList());
            OwnersParent.ForEach(O => ListOfOwners.Add(new SelectListItem { Value = O.Id.ToString(), Text = O.Name }));


        }


        public async Task<IActionResult> OnPostSave([FromBody] AssignUserOwnerCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = "تعین موقف به کاربر موفقانه انجام شد.",
                    Description = string.Empty

                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSearch([FromBody] SearchAssignOwnerQuery query)
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
