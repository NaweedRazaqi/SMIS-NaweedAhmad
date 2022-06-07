using App.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
   public class ScheduleExam
    {

        public int Id { get; set; }
        public string ExmName { get; set; }
        public short SubjectId { get; set; }
        public short SchoolTypeId { get; set; }
        public int StudentClassId { get; set; }
        public short ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public TimeSpan? ExamTimeStart { get; set; }
        public TimeSpan? ExamTimeEnd { get; set; }
        public DateTime? ExamDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual SchoolType SchoolType { get; set; }
        public virtual SubjectManagement Subject { get; set; }
    }
}
