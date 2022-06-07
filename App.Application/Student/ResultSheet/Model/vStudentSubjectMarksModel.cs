using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.ResultSheet.Model
{
    public class vStudentSubjectMarksModel
    {

        public long? MarksId { get; set; }
        public long? Marks { get; set; }
        public long ProfileId { get; set; }
        public string StudentSubjects { get; set; }
        public int? StudentExamType { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string StudentSchool { get; set; }
        public string StudentClass { get; set; }
    }
}
