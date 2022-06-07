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
using Clean.Application.Documents.Queries;
using Clean.Application.System.Queries;
using Clean.Common;
using Clean.Common.Enums;
using Clean.Common.Storage;
using Clean.UI.Types;
using Clean.UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clean.UI.Pages.Student.Profile
{
    public class StudentTerminationModel : BasePage

    {
        public string ScreenIDEcyption { get; set; }
        public int ScreenID = 23;
        public async Task OnGetAsync([FromRoute] int id)
        {
            ListOfClassTypes = new List<SelectListItem>();
            var classes = await Mediator.Send(new GetClassTypeList());
            classes.ForEach(e => ListOfClassTypes.Add(new SelectListItem { Value = e.Id.ToString(), Text = e.DariName }));
            ScreenIDEcyption = EncryptionHelper.Encrypt(id.ToString());

            ListOfDocumentTypes = new List<SelectListItem>();
            var documentTypes = await Mediator.Send(new GetDocumentTypeQuery() { ScreenID = ScreenID, Catagory = "C" });
            foreach (var documentType in documentTypes)
                ListOfDocumentTypes.Add(new SelectListItem() { Text = documentType.Name, Value = documentType.Id.ToString() });
        }
        public async Task<IActionResult> OnPostSave([FromBody] StudentTerminationCommand command)
        {
            try
            {
                IEnumerable<StudentsTerminationModel> SaveResult = new List<StudentsTerminationModel>();
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchStudentTerminationQuery query)
        {
            var result = new JsonResult(null);
            try
            {

                IEnumerable<StudentsTerminationModel> SaveResult = new List<StudentsTerminationModel>();

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


        public async Task<IActionResult> OnPostUpload([FromForm] IFormFile img, [FromForm] string UploadType)
        {
            FileStorage _storage = new FileStorage();
            var extension = System.IO.Path.GetExtension(img.FileName);
            // check for a valid mediatype
            if (!img.ContentType.StartsWith("image/"))
            {
                return new JsonResult(new UIResult()
                {
                    Data = null,
                    Status = 0,
                    Text = "فارمت عکس درست نیست",
                    // Can be changed from app settings
                    Description = ""
                });
            }
            else
            {
                string basePath = "";
                if (UploadType == UploadTypes.Photo)
                {
                    basePath = AppConfig.ImagesPath;
                }
                else if (UploadType == UploadTypes.Signature)
                {
                    basePath = AppConfig.SignaturesPath;
                }
                var additional = DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                string filename = await _storage.CreateAsync(img.OpenReadStream(), extension, basePath + additional);
                var result = new
                {
                    status = "success",
                    url = additional + filename
                };
                return new JsonResult(result);
            }
        }


        public async Task<IActionResult> OnPostCrop([FromBody] CropRequest cropmodel)
        {
            FileStorage _storage = new FileStorage();
            var basePath = "";
            if (cropmodel.uploadType == UploadTypes.Photo)
            {
                basePath = AppConfig.ImagesPath;
            }
            else if (cropmodel.uploadType == UploadTypes.Signature)
            {
                basePath = AppConfig.SignaturesPath;
            }
            var additional = DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            var crs = await _storage.Crop(cropmodel, basePath, additional);
            object result;
            if (crs.Success)
            {
                result = new
                {
                    status = "success",
                    url = additional + crs.ToPath
                };
            }
            else
            {
                result = new
                {
                    status = "fail",
                    url = "",
                    message = crs.ErrorMsg
                };
            }
            return new JsonResult(result);
        }


        public async Task<IActionResult> OnGetDownload([FromQuery] string file, [FromQuery] string uploadType)
        {
            FileStorage _storage = new FileStorage();
            var basePath = "";
            if (uploadType == UploadTypes.Photo)
            {
                basePath = AppConfig.ImagesPath;
            }
            else if (uploadType == UploadTypes.Signature)
            {
                basePath = AppConfig.SignaturesPath;
            }
            var filepath = basePath + file;
            System.IO.Stream filecontent = await _storage.GetAsync(filepath);
            var filetype = _storage.GetContentType(filepath);
            return File(filecontent, filetype, file);
        }

    }
}
