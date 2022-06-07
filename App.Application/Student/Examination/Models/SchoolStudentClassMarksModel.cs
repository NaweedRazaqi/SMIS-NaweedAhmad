using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.Models
{
   public class SchoolStudentClassMarksModel
    {
     public int Id { get; set; }
     public int SubjectId { get; set; }
     public int Marks { get; set; }
     public int StudentClassId { get; set; }
    public int? ExamTypeID { get; set; }
     public List<long> MarkId { get; set; }
     public int? ExamTypeId { get; set; }
    }
}
