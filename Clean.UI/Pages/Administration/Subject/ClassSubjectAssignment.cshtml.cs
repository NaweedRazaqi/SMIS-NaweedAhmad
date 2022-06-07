using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Application.Administration.Subject.Commands;
using App.Application.Administration.Subject.Models;
using App.Application.Administration.Subject.Queries;
using App.Application.Lookup.Queries;
using Clean.Common.Models;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Administration.Subject
{
    public class ClassSubjectAssignmentModel : BasePage
    {
        public async  Task OnGetAsync()
        {
            ListOfClassManagements = new List<SelectListItem>();
            var ClassManagement = await Mediator.Send(new GetClassManagementList());
            ClassManagement.ForEach(e => ListOfClassManagements.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfClassTypes = new List<SelectListItem>();
            var ClassType = await Mediator.Send(new GetClassTypeList());
            ClassType.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));

            //ListOfYears = new List<SelectListItem>();
            //var Year = await Mediator.Send(new GetYearList());
            //Year.ForEach(e => ListOfYears.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            //ListOfTeachers = new List<SelectListItem>();
            //var Teacher = await Mediator.Send(new GetTeacherList());
            //Teacher.ForEach(e => ListOfTeachers.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.FullName }));

            ListOfSubjectManagements = new List<SelectListItem>();
            var SubjectManagement = await Mediator.Send(new GetSubjectManagementList());
            SubjectManagement.ForEach(e => ListOfSubjectManagements.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.Name }));

            ListOfSchoolTypes = new List<SelectListItem>();
            var schooltype = await Mediator.Send(new GetSchoolType());
            schooltype.ForEach(e => ListOfSchoolTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.NameDari }));
        }

        public async Task<IActionResult> OnPostSubjectType([FromBody] DynamicListModel Data)
        {
            var result = new JsonResult(null);
            try
            {
                List<object> SearchResult = new List<object>();
                var subjects = await Mediator.Send(new GetSubjectLists() { SchoolTypeID = Data.ID });
                foreach (var s in subjects)
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
        public async Task<IActionResult> OnPostSave([FromBody] CreateClassSubjectAssignment command)
        {
            try
            {
                IEnumerable<SearchSubjectAssignmentModel> SaveResult = new List<SearchSubjectAssignmentModel>();
                SaveResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = SaveResult },
                    Status = UIStatus.Success,
                    Text = "مضمون موفقانه به صنف تعیین گردید!",
                    Description = string.Empty
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
        public async Task<IActionResult> OnPostSearch([FromBody] SearchSubjectAssigmentQueries query)
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
