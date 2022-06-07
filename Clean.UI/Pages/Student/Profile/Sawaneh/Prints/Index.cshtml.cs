using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Student.Sawaneh.Queries;
using App.Application.Subject.Queries;
using App.Persistence.Context;
using Clean.Common;
using Clean.Common.Storage;
using Clean.Persistence.Services;
using Clean.UI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Clean.UI.Pages.Student.Profile.Sawaneh.Prints
{
    public class IndexModel : BasePage
    {

        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string PermenentLocation { get; set; }
        public string FatherProfession { get; set; }
        public string StudentHealth { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Reasons { get; set; }
        public int? Fine { get; set; }
        public string PhotoPath { get; set; }
        public string Code { get; set; }
        public string RelativeName { get; set; }
        public string Classtype { get; set; }
        public string DobShamsi { get; set; }
        public string MotherLanguage { get; set; }
        public string termdateShamsi { get; set; }
        public string TdocumentNo { get; set; }
        public short? TerminatedClass { get; set; }
        public string TerminatedClassText { get; set; }
        public List<string> subjectlists { get; set; }
        public string SubjectName { get; set; }
        public List<SubjectResult> SubjectResults = new List<SubjectResult>();

        private AppDbContext context;


        private readonly IConfiguration Config;
        public IndexModel(IConfiguration configuration, ICurrentUser currentUser, AppDbContext context)
        {
            Config = configuration;
            this.context = context;
        }


        public async Task OnGet([FromQuery] long recordId)
        {

            var result = await this.Mediator.Send(new SearchStudentSawanehQuery { Id = recordId });
            var cur = result.FirstOrDefault();

            if (!result.Any())
            {
                FirstName = "معلومات موجود نمیباشد";
                FatherName = "معلومات موجود نمیباشد";
                GrandFatherName = "معلومات موجود نمیباشد";
                PermenentLocation = "معلومات موجود نمیباشد";
                FatherProfession = "معلومات موجود نمیباشد";
                Code = "معلومات موجود نمیباشد";
                RelativeName = "معلومات موجود نمیباشد";
                Classtype = "معلومات موجود نمیباشد";
                DobShamsi = "معلومات موجود نمیباشد";
                MotherLanguage = "معلومات موجود نمیباشد";
                Reasons = "معلومات موجود نمیباشد";
                Fine = 000;
                PhotoPath = "موجود نیست";
                TdocumentNo = "موجود نیست";
                TerminatedClassText = "موجود نیست";
            }
            else
            {
                FirstName = cur.FirstName;
                FatherName = cur.FatherName;
                GrandFatherName = cur.GrandFatherName;
                PermenentLocation = cur.PermenentLocation;
                FatherProfession = cur.FatherProfession;
                Code = cur.Code;
                RelativeName = cur.RelativeName;
                Classtype = cur.Classtype;
                DobShamsi = cur.DobShamsi;
                MotherLanguage = cur.MotherLanguage;
                Reasons = cur.Reasons;
                Fine = cur.Fine;
                TerminationDate = cur.TerminationDate;
                StudentHealth = cur.StudentHealth;
                termdateShamsi = cur.termdateShamsi;
                PhotoPath = await GetFile("ProfilePhotos", cur.PhotoPath);
                TerminatedClass = cur.TerminatedClass;
                TdocumentNo = cur.TdocumentNo;
                TerminatedClassText = cur.TerminatedClassText;
                var subjectList = await this.Mediator.Send(new SearchSubjectQuery { });
                List<SubjectMarks> subjectResList = new List<SubjectMarks>();
                var classList = context.ClassTypes.ToList();

                foreach (var subItem in subjectList)
                {
                    SubjectMarks subjectRes = new SubjectMarks();
                    subjectRes.Subject = subItem.Name;
                    subjectRes.SubjectID = subItem.Id;

                    List<ClassMarks> classMarksList = new List<ClassMarks>();

                    foreach (var classListItem in classList)
                    {
                        var TotalMark = 0;
                        var subMarkList = from m in context.StudentClassMarks
                                          join s in context.StudentClasses on m.StudentClassId equals s.Id
                                          join c in context.ClassTypes on s.ClassTypeId equals c.Id
                                          where m.SubjectId == subItem.Id && s.ProfileId == cur.Id && s.ClassTypeId == classListItem.Id
                                          select new
                                          {
                                              subItem.Id,
                                              m.Marks,
                                              s.ClassTypeId,
                                              c.Name,
                                          };

                        foreach (var subMarkListItem in subMarkList)
                        {
                            TotalMark = TotalMark + subMarkListItem.Marks;
                        }
                        ClassMarks classMarks = new ClassMarks();

                        classMarks.ClassId = classListItem.Id;
                        classMarks.ClassName = classListItem.Name;
                        classMarks.Score = TotalMark;
                        classMarksList.Add(classMarks);
                    }
                    subjectRes.ClassMarks = classMarksList;
                    subjectResList.Add(subjectRes);
                }

                foreach (var sItem in subjectResList)
                {
                    var curitem = SubjectResults.Where(e => e.Subject == sItem.Subject).SingleOrDefault();
                    if (curitem == null)
                    {
                        curitem = new SubjectResult();

                        curitem.Subject = sItem.Subject;
                        curitem.SubjectID = sItem.SubjectID;
                        curitem.FirstClass = sItem.ClassMarks[0].Score == 0 ? curitem.FirstClass = null : curitem.FirstClass = sItem.ClassMarks[0].Score;
                        curitem.SecondClass = sItem.ClassMarks[1].Score == 0 ? curitem.SecondClass = null : curitem.SecondClass = sItem.ClassMarks[1].Score;
                        curitem.ThirdClass = sItem.ClassMarks[2].Score == 0 ? curitem.ThirdClass = null : curitem.ThirdClass = sItem.ClassMarks[2].Score;
                        curitem.FurtClass = sItem.ClassMarks[3].Score == 0 ? curitem.FurtClass = null : curitem.FurtClass = sItem.ClassMarks[3].Score;
                        curitem.FivClass = sItem.ClassMarks[4].Score == 0 ? curitem.FivClass = null : curitem.FivClass = sItem.ClassMarks[4].Score;
                        curitem.SixClass = sItem.ClassMarks[5].Score == 0 ? curitem.SixClass = null : curitem.SixClass = sItem.ClassMarks[5].Score;
                        curitem.SevClass = sItem.ClassMarks[6].Score == 0 ? curitem.SevClass = null : curitem.SevClass = sItem.ClassMarks[6].Score;
                        curitem.EiClass = sItem.ClassMarks[7].Score == 0 ? curitem.EiClass = null : curitem.EiClass = sItem.ClassMarks[7].Score;
                        curitem.NiClass = sItem.ClassMarks[8].Score == 0 ? curitem.NiClass = null : curitem.NiClass = sItem.ClassMarks[8].Score;
                        curitem.TenClass = sItem.ClassMarks[9].Score == 0 ? curitem.TenClass = null : curitem.TenClass = sItem.ClassMarks[9].Score;
                        curitem.EleClass = sItem.ClassMarks[10].Score == 0 ? curitem.EleClass = null : curitem.EleClass = sItem.ClassMarks[10].Score;
                        curitem.TowClass = sItem.ClassMarks[11].Score == 0 ? curitem.TowClass = null : curitem.TowClass = sItem.ClassMarks[11].Score;

                        SubjectResults.Add(curitem);
                    }
                }
            }
        }
        public async Task<string> GetFile(String Dir, String FileName)
        {
            FileStorage _storage = new FileStorage();
            var filepath = Config[Dir] + FileName;
            var dirpath = AppConfig.ImagesPath;
            var fullpath = dirpath + filepath;
            System.IO.Stream filecontent = await _storage.GetAsync(fullpath);

            byte[] filebytes = new byte[filecontent.Length];
            filecontent.Read(filebytes, 0, Convert.ToInt32(filecontent.Length));
            String Result = "data:" + _storage.GetContentType(filepath) + ";base64," + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            return Result;
        }


        public class SubjectResult
        {
            public string Subject { get; set; }
            public long? SubjectID { get; set; }
            public decimal? FirstClass { get; set; }
            public decimal? SecondClass { get; set; }
            public decimal? ThirdClass { get; set; }
            public decimal? FurtClass { get; set; }
            public decimal? FivClass { get; set; }
            public decimal? SixClass { get; set; }
            public decimal? SevClass { get; set; }
            public decimal? EiClass { get; set; }
            public decimal? NiClass { get; set; }
            public decimal? TenClass { get; set; }
            public decimal? EleClass { get; set; }
            public decimal? TowClass { get; set; }

        }

        public class SubjectMarks
        {
            public string Subject { get; set; }
            public long? SubjectID { get; set; }
            public List<ClassMarks> ClassMarks { get; set; }
        }
        public class ClassMarks
        {
            public string ClassName { get; set; }
            public long? ClassId { get; set; }
            public decimal Score { get; set; }
        }
    }




}
