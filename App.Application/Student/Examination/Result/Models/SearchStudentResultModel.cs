using App.Application.Student.Prf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.Result.Models
{
    public class SearchStudentResultModel
    {

        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string SchoolName { get; set; }
        public string ClassTypeName { get; set; }
        public string ClassCategory { get; set; }
        public string ClassCategoryId { get; set; }
        public short ClassTypeId { get; set; }
        public int schoolId { get; set; }
        public Boolean? IsActive { get; set; }
        public short ClassManagementID { get; set; }
        public int ExamTypeId { get; set; }
        public List<StudentClassSubjectModel> ClassSubject { get; set; }
        public string Marks { get; set; }
        public int? ProfileId { get; set; }
    }
}
