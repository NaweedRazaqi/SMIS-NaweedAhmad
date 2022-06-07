using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
   public class SearchStudentClassModel
    {
        public int? Id { get; set; }
        public decimal? ProfileId { get; set; }
        public string StudentFatherName { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentCode { get; set; }
        public string StudentLastName { get; set; }
        public string StudentGrandFatherName { get; set; }
        public string StudentFirstNameEng { get; set; }

        public string StudentFatherNameEng { get; set; }

        public int? StudentSchoolID { get; set; }

        public string StudentSchoolName { get; set; }
        public int? ClassTypeId { get; set; }
        public string ClassTypeName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public long? ClassManagementId { get; set; }
        public string ClassManagementName { get; set; }

        public string Statuse { get; set; }

        public int totalMarks { get; set; }
        public int marks { get; set; }
        public List<StudentClassSubjectModel> ClassSubject { get; set; }






    }
}
