using System;
using System.Collections.Generic;
using App.Domain.Entity.prf;

namespace App.Domain.Entity.look
{
    public partial class ClassType
    {
        public ClassType()
        {
            ClassManagement = new HashSet<ClassManagement>();
            StudentRegistration = new HashSet<StudentRegisteration>();
            StudentClass = new HashSet<StudentClass>();
            ClassSubjectManagements = new HashSet<ClassSubjectManagement>();
            ScheduleExams = new HashSet<ScheduleExam>();
            StudentResults = new HashSet<StudentResult>();
            ClassUpgradations = new HashSet<ClassUpgradation>();
          
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string DariName { get; set; }
        public string PashtoName { get; set; }

        public virtual ICollection<ClassManagement> ClassManagement { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }
        public virtual ICollection<ClassSubjectManagement> ClassSubjectManagements { get; set; }
        public virtual ICollection<ScheduleExam> ScheduleExams { get; set; }
        public virtual ICollection<StudentResult> StudentResults { get; set; }
        public virtual ICollection<ClassUpgradation> ClassUpgradations { get; set; }
    
    }
}
