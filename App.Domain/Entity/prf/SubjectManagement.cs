using App.Domain.Entity.look;
using System;
using System.Collections;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class SubjectManagement
    {
        public SubjectManagement()
        {
            ClassSubjectManagement = new HashSet<ClassSubjectManagement>();
            StudentClassMarks = new HashSet<StudentClassMarks>();
            SubjectAssignments = new HashSet<SubjectAssignment>();
            ScheduleExams = new HashSet<ScheduleExam>();
        }
        public short Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public short? StatusId { get; set; }
        public int? ViewOrder { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public short? SchoolTypeId { get; set; }

        public virtual SchoolType SchoolType { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<ClassSubjectManagement> ClassSubjectManagement { get; set; }
        public virtual ICollection<StudentClassMarks> StudentClassMarks { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }
        public virtual ICollection<ScheduleExam> ScheduleExams { get; set; }
    }
}
