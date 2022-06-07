
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using App.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Queries
{
    public class SearchStudentClassMainQuery : IRequest<List<SearchStudentClassModel>>
    {
        public decimal? ID { get; set; }
        public string StudentCode { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentFatherName { get; set; }
        public decimal? ProfileID { get; set; }
        public decimal? StudentClassId { get; set; }
    }
    public class SearchStudentClassMainQueryHandler : IRequestHandler<SearchStudentClassMainQuery, List<SearchStudentClassModel>>
    {
        private readonly AppDbContext context;
        public SearchStudentClassMainQueryHandler(AppDbContext mContext)
        {
            context = mContext;
        }
        public async Task<List<SearchStudentClassModel>> Handle(SearchStudentClassMainQuery request, CancellationToken cancellationToken)
        {

            //var profielid = await context.Profiles.Select(e => new SearchProfileModel { Id = e.Id }).ToListAsync();
            //var classId =  context.StudentClasses.Select(e => new SearchStudentClassModel { Id = e.Id }).ToListAsync();
            // var profile = request.Id.HasValue ? context.Profiles.Where(e => e.Id == request.Id).Single() : new Domain.Entity.prf.Profile();
            //var classId = request.StudentClassId.HasValue == null ? context.StudentClasses.Select(s => s.Id).Single():
            //var convertClassId =  Convert.ToDecimal(classId);
            //var classsubjectmId = await context.ClassSubjectManagements.Select(e => new ClassSubjectManagement { Id = e.Id }).ToListAsync();
            //var subjectId = await context.SubjectManagements.Select(e => new SearchSubjectModel { Id = e.Id }).ToListAsync();

            //var marks = await context.highSchoolStudentClassMarks.Select(e => new HighSchoolStudentClassMarks { Id = e.Id, Marks = e.Marks })
            //                                                     .Where(e => e.StudentClassId == convertClassId).ToListAsync();
            // .Where(e =>e.StudentClassId==context.StudentClasses.Select(e =>new SearchStudentClassModel {Id=e.Id }).SingleOrDefault()).ToListAsync();



            var query = context.StudentClasses.AsQueryable();
            if (request.ID.HasValue)
            {
                query = query.Where(e => e.Id == request.ID);
            }
            if (request.ProfileID.HasValue)
            {
                query = query.Where(e => e.ProfileId == request.ProfileID);
            }
            if (!String.IsNullOrEmpty(request.StudentCode))
            {
                query = query.Where(e => e.Profile.Code == request.StudentCode);
            }

            if (!String.IsNullOrEmpty(request.StudentFirstName))
            {
                query = query.Where(e => EF.Functions.Like(e.Profile.FirstName, String.Concat("%", request.StudentFirstName, "%")));
            }

            if (!String.IsNullOrEmpty(request.StudentFatherName))
            {
                query = query.Where(e => EF.Functions.Like(e.Profile.FatherName, String.Concat("%", request.StudentFatherName, "%")));
            }

            //var result = from o in context.highSchoolStudentClassMarks//.Where(d => d.memberid == d.membertable.id && d.entrydate >= thisMonthStart && d.entrydate <= thisMonthEnd)
            //             group o by new { o.Id } into total
            //             select new
            //             {
            //                 //membername = purchasegroup.Key,
            //                 //total = purchasegroup.Sum(s => s.price)
            //                 marks=total.Sum(marks)
            //             };
            List<HighSchoolStudentClassMarks> listItems = new List<HighSchoolStudentClassMarks>();
            var items = context.StudentClassMarks.Select(s => new HighSchoolStudentClassMarks
            {
                Id = s.Id,
                SubjectId = s.SubjectId,
                Marks = s.Marks
            }).ToList();
            //foreach (var x in items)
            //{
            //    listItems.Add(new StudentClassSubjectModel { Id = x.Select(o => o.Id).FirstOrDefault(),  Name = x.Select(o => o.SubjectId).FirstOrDefault(), Marks = x.Select(o => o.Marks).Sum() });
            //}


            //(from db in context.highSchoolStudentClassMarks
            //             group db by db.SubjectId into sub
            //             select sub).ToList();

            return await query.Include(e => e.Profile)
                .Include(e => e.ClassType)
                .Include(e => e.ClassManagement)
                .Select(e => new SearchStudentClassModel
                {
                  //  Id = e.Id,
                    ProfileId = e.ProfileId,
                    StudentFatherName = e.Profile.FatherName,
                    StudentFirstName = e.Profile.FirstName,
                    StudentCode = e.Profile.Code,
                    StudentLastName = e.Profile.LastName,
                    StudentFirstNameEng = e.Profile.FirstNameEng,
                    StudentFatherNameEng = e.Profile.FatherNameEng,
                    //StudentSchoolID = e.Profile.School.Id,
                    //StudentSchoolName = e.Profile.School.Name,
                    ClassTypeId = e.ClassTypeId,
                    ClassTypeName = e.ClassType.DariName,
                    ClassManagementId = e.ClassManagementId,
                    ClassManagementName = e.ClassManagement.Name

                 //  , marks = (context.highSchoolStudentClassMarks.Where(m => m.SubjectId == e.SubjectId)).Select().Single()

                    //(context.highSchoolStudentClassMarks.Where(d => d.SubjectId == e.SubjectId && d.StudentClassId == q.Id).Select(e => e.Marks).SingleOrDefault() == null ? 0 : AppDbContext.highSchoolStudentClassMarks.Where(d => d.SubjectId == e.SubjectId && d.StudentClassId == q.Id).Select(e => e.Marks).Single()),

                    //,totalMarks=marks


                }).ToListAsync();
        }
    }

}
