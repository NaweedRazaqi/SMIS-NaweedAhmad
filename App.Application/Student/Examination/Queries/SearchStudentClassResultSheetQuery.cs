using App.Application.Student.Examination.Models;
using App.Persistence.Context;
using Clean.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Student.Examination.Queries
{
    public class SearchStudentClassResultSheetQuery : IRequest<IEnumerable<StudentClassResultSheetModel>>
    {
        public int Id { get; set; }
        public int StudentSchoolId { get; set; }
        public int ClassTypeId { get; set; }
        public int ClassManagementId { get; set; }
        public short SchoolTypeId { get; set; }
    }
    public class SearchSubjectQueryHandler : IRequestHandler<SearchStudentClassResultSheetQuery, IEnumerable<StudentClassResultSheetModel>>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly ICurrentUser currentUser;

        public SearchSubjectQueryHandler(AppDbContext context, IMediator mediator, ICurrentUser currentUser)
        {
            this.context = context;
            this.mediator = mediator;
            this.currentUser = currentUser;
        }
        public async Task<IEnumerable<StudentClassResultSheetModel>> Handle(SearchStudentClassResultSheetQuery request, CancellationToken cancellationToken)
        {

            List<StudentClassResultSheetModel> Result = null;
            IEnumerable<StudentClassResultSheetModel> Total_Result = null;

            var query = context.StudentClassResultSheets.AsQueryable();
            int currentuserid = await currentUser.GetUserId();
            var OwnerID = context.UserOwners.Where(u => u.UserId == currentuserid && u.IsActive == true).Select(c => c.OwnerId).Single();
            var OwnerIDChild1 = context.AspNetOwners.Where(u => u.ParentId == OwnerID | u.Id == OwnerID).Select(u => u.Id).ToList();
            var OwnerIDChild2 = context.AspNetOwners.Where(u => u.ParentId == OwnerID).Select(u => u.Id).ToList();

            if (OwnerIDChild2.Count() == 0)
            {
                foreach (var p in OwnerIDChild1)
                {
                    if (request.Id != 0)
                    {
                        query = query.Where(e => e.Id == request.Id);
                    }
                    if (request.ClassManagementId != 0)
                    {
                        query = query.Where(e => e.ClassManagementId == request.ClassManagementId);
                    }
                    if (request.SchoolTypeId != 0)
                    {
                        query = query.Where(s => s.SchoolTypeId == request.SchoolTypeId);
                    }
                    if (request.ClassTypeId != 0)
                    {
                        query = query.Where(e => e.ClassTypeId == request.ClassTypeId);
                    }
                    Result =  query.Where(d => d.OwnerId == p).Select(R => new StudentClassResultSheetModel
                    {
                        Id = R.Id,
                        SchoolTypeId = R.SchoolTypeId,
                        StudentSchoolId = R.StudentSchoolId,
                        StudentSchoolName = R.StudentSchool.Dari,
                        SchoolTypeName = R.SchoolType.NameDari,
                        ClassTypeName = R.ClassType.DariName,
                        ClassManagementName = R.ClassManagement.Name,
                        ClassTypeId = R.ClassTypeId,
                        DocumentTypeName = R.DocumentType.Name,
                        ClassManagementId = R.ClassManagementId,
                        DocumentTypeId = (int)R.DocumentTypeId,
                        DocumentName = (context.Documents.Where(D => D.DocumentTypeId == R.DocumentTypeId && D.RecordId == R.Id).Select(D => D.Path).SingleOrDefault()) == null ? "شوقه ضمیمه نشده است" : "شوقه ضمیمه شده است",
                    }).ToList();
                    if (Total_Result == null)
                    {
                        Total_Result = Result;
                    }
                    if (Total_Result != null)
                    {
                        Total_Result = Total_Result.Concat(Result).Distinct();
                    }
                }
                return Total_Result.ToList();
            }
            else
            {
                var OwnerIDChild11 = context.AspNetOwners.Where(u => u.ParentId == OwnerID).Select(u => u.Id).ToList();
                foreach(var p in OwnerIDChild11)
                {
                    var OwnerIDChild2s = context.AspNetOwners.Where(u => u.ParentId == p | u.Id == p).Select(u => u.Id).ToList();
                    foreach(var cp in OwnerIDChild2s)
                    {
                        if (request.Id != 0)
                        {
                            query = query.Where(e => e.Id == request.Id);
                        }
                        if (request.ClassManagementId != 0)
                        {
                            query = query.Where(e => e.ClassManagementId == request.ClassManagementId);
                        }
                        if (request.SchoolTypeId != 0)
                        {
                            query = query.Where(s => s.SchoolTypeId == request.SchoolTypeId);
                        }
                        if (request.ClassTypeId != 0)
                        {
                            query = query.Where(e => e.ClassTypeId == request.ClassTypeId);
                        }
                        Result = query.Where(d => d.OwnerId == cp).Select(R => new StudentClassResultSheetModel
                        {
                            Id = R.Id,
                            SchoolTypeId = R.SchoolTypeId,
                            StudentSchoolId = R.StudentSchoolId,
                            StudentSchoolName = R.StudentSchool.Dari,
                            SchoolTypeName = R.SchoolType.NameDari,
                            ClassTypeName = R.ClassType.DariName,
                            ClassManagementName = R.ClassManagement.Name,
                            ClassTypeId = R.ClassTypeId,
                            DocumentTypeName = R.DocumentType.Name,
                            ClassManagementId = R.ClassManagementId,
                            DocumentTypeId = (int)R.DocumentTypeId,
                            DocumentName = (context.Documents.Where(D => D.DocumentTypeId == R.DocumentTypeId && D.RecordId == R.Id).Select(D => D.Path).SingleOrDefault()) == null ? "شوقه ضمیمه نشده است" : "شوقه ضمیمه شده است",
                        }).ToList();
                        if (Total_Result == null)
                        {
                            Total_Result = Result;
                        }
                        if (Total_Result != null)
                        {
                            Total_Result = Total_Result.Concat(Result).Distinct();
                        }
                    }
                }

                if (request.Id != 0)
                {
                    query = query.Where(e => e.Id == request.Id);
                }
                if (request.ClassManagementId != 0)
                {
                    query = query.Where(e => e.ClassManagementId == request.ClassManagementId);
                }
                if (request.SchoolTypeId != 0)
                {
                    query = query.Where(s => s.SchoolTypeId == request.SchoolTypeId);
                }
                if (request.ClassTypeId != 0)
                {
                    query = query.Where(e => e.ClassTypeId == request.ClassTypeId);
                }
                Result = query.Where(d => d.OwnerId == OwnerID).Select(R => new StudentClassResultSheetModel
                {
                    Id = R.Id,
                    SchoolTypeId = R.SchoolTypeId,
                    StudentSchoolId = R.StudentSchoolId,
                    StudentSchoolName = R.StudentSchool.Dari,
                    SchoolTypeName = R.SchoolType.NameDari,
                    ClassTypeName = R.ClassType.DariName,
                    ClassManagementName = R.ClassManagement.Name,
                    ClassTypeId = R.ClassTypeId,
                    DocumentTypeName = R.DocumentType.Name,
                    ClassManagementId = R.ClassManagementId,
                    DocumentTypeId = (int)R.DocumentTypeId,
                    DocumentName = (context.Documents.Where(D => D.DocumentTypeId == R.DocumentTypeId && D.RecordId == R.Id).Select(D => D.Path).SingleOrDefault()) == null ? "شوقه ضمیمه نشده است" : "شوقه ضمیمه شده است",
                }).ToList();
                if (Total_Result == null)
                {
                    Total_Result = Result;
                }
                if (Total_Result != null)
                {
                    Total_Result = Total_Result.Concat(Result).Distinct();
                }
                return Total_Result.ToList();
            }
         
        }

        }
        }
    

