using System;
using System.Collections.Generic;
using App.Domain.Entity.look;

namespace App.Domain.Entity.prf
{
    public partial class ClassManagement
    {
        public ClassManagement()
        {
            StudentClass = new HashSet<StudentClass>();
            StudentRegistration = new HashSet<StudentRegisteration>();
            ScheduleExams = new HashSet<ScheduleExam>();
            StudentResults = new HashSet<StudentResult>();
            ClassUpgradations = new HashSet<ClassUpgradation>();
        }

        public short Id { get; set; }
        public short? ClassTypeId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public short? SchoolId { get; set; }
        public short? Year { get; set; }
        public DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ClassType ClassType { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
        public virtual ICollection<ScheduleExam> ScheduleExams { get; set; }
        public virtual ICollection<StudentResult> StudentResults { get; set; }
        public virtual ICollection<ClassUpgradation> ClassUpgradations { get; set; }
    }
}
