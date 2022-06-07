using App.Application.Prf.Models;
using App.Application.Student.Prf.Models;
using App.Persistence.Context;
using Clean.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.SearchStudentSchool
{

    public class SearchStudentSchoolQuery : IRequest<List<SearchStudentSchoolProfileModel>>
    {
        public int StudentSchoolID { get; set; }
        public int ClassTypeID { get; set; }
        public long ClassManangementID { get; set; }
        public int ExamTypeID { get; set; }
    }

    public class SearchStudentSchoolQueyrHandler : IRequestHandler<SearchStudentSchoolQuery, List<SearchStudentSchoolProfileModel>>
    {
        public SearchStudentSchoolQueyrHandler(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private AppDbContext AppDbContext { get; }

        public async Task<List<SearchStudentSchoolProfileModel>> Handle(SearchStudentSchoolQuery request, CancellationToken cancellationToken)
        {
            //var query = AppDbContext.StudentClasses.AsQueryable();
            //var query1 = AppDbContext.Profiles.AsQueryable();
            //if (request.StudentSchoolID == null)
            //{
            //    throw new BusinessRulesException("ستیسبتشستبمیستبمشیستبم");
            //}
            //else
            //{
                var query1 = AppDbContext.StudentClasses.AsQueryable();
                var query2 = AppDbContext.StudentClassMarks.AsQueryable();
                if (request.StudentSchoolID != 0)
                    query1 = query1.Where(e => e.SchoolId == request.StudentSchoolID);

                if (request.ClassTypeID != 0)
                    query1 = query1.Where(x => x.ClassTypeId == request.ClassTypeID);
                if (request.ClassManangementID != 0)
                    query1 = query1.Where(x => x.ClassManagementId == request.ClassManangementID);

                if (request.ExamTypeID != 0)
                {
                    query2 = query2.Where(t => t.Id == request.ExamTypeID);
                }
                return await query1.Select(q => new SearchStudentSchoolProfileModel
                {

                    schoolId = q.School.Id,
                    Id = q.Id,
                    FirstName = q.Profile.FirstName,
                    FatherName = q.Profile.FatherName,
                    SchoolName = q.School.Dari,
                    ClassTypeName = q.ClassType.DariName,
                    ClassCategory = q.ClassManagement.Name,
                    ClassManagementID = (int)q.ClassManagement.Id,
                    ExamTypeId = AppDbContext.ExamTypes.Where(e => e.Id == request.ExamTypeID).Select(s => s.Id).Single(),
                    ClassCategoryId = q.ClassManagement.Id.ToString(),
                    ClassSubject = AppDbContext.ClassSubjectManagements.Where(e => e.ClassTypeId == request.ClassTypeID).Select(e => new StudentClassSubjectModel
                    {
                        Id = e.Subject.Id,
                        Name = e.Subject.Name,
                        Marks = (AppDbContext.StudentClassMarks.Where(d => d.SubjectId == e.SubjectId && d.StudentClassId == q.Id && d.ExamTypeId == request.ExamTypeID
                       ).Select(e => e.Marks).SingleOrDefault() == null ? 0 : AppDbContext.StudentClassMarks.Where(d => d.SubjectId == e.SubjectId && d.StudentClassId == q.Id &&
                       d.ExamTypeId == request.ExamTypeID).Select(e => e.Marks).Single()),

                        HSSCMId = AppDbContext.StudentClassMarks.Where(d => d.SubjectId == e.SubjectId && d.StudentClassId == q.Id).Select(e => e.Id).SingleOrDefault()
                    }).ToList()
                }).Where(xx => xx.schoolId == request.StudentSchoolID).ToListAsync();
            }
        }
    //}
}
