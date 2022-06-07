using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.ScheduleExam.Models
{
   public class SearchScheduleExamModel
    {

        public int Id { get; set; }
        public string ExmName { get; set; }
        public short SchoolTypeId { get; set; }
        public long SubjectId { get; set; }
        public int StudentClassId { get; set; }
        public int ClassTypeId { get; set; }
        public long? ClassManagementId { get; set; }
        public TimeSpan? ExamTimeStart { get; set; }
        public TimeSpan? ExamTimeEnd { get; set; }
        public DateTime? ExamDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string dateShamsi { get; set; }
        public string SubjectText { get; set; }
        public string ClasstypeText { get; set; }
        public string ClassmanagementText { get; set; }
        public string SchoolTypeText { get; set; }
    }
}
