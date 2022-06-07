using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public class ResultSheet
    {

        public int ID { get; set; }
        public int? ProfileId { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string SchoolName { get; set; }
        public string Directorate { get; set; }
        public string SubjectName { get; set; }
        public int Marks { get; set; }
        public int avg { get; set; }
        public int Total { get; set; }
        public string Result { get; set; }
        public string ClassGrad { get; set; }
        public int PresentDays { get; set; }
        public int UpsentDays { get; set; }
        public int SikDays { get; set; }
        public int LeaveDays { get; set; }
    }
}
