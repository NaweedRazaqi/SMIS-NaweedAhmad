
using App.Application.Student.Prf.Models;
using System.Collections.Generic;

namespace App.Application.Student.Examination.SearchStudentSchool
{
    public class SearchStudentSchoolProfileModel
    {
        public decimal? Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string SchoolName { get; set; }
        public string ClassTypeName { get; set; }
        public string ClassCategory { get; set; }
        public string ClassCategoryId { get; set; }
        public int schoolId { get; set; }
        public int ClassManagementID { get; set; }
        public int ExamTypeId { get; set; }
        public List<StudentClassSubjectModel> ClassSubject { get; set; }
        public string Marks { get; set; }


    }
}
