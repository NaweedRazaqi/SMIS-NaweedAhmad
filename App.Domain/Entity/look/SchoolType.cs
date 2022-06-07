using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class SchoolType
    {
        public SchoolType()
        {
            School = new HashSet<School>();
            StudentRegistration = new HashSet<StudentRegisteration>();
            ScheduleExams = new HashSet<ScheduleExam>();
            SubjectAssignments   = new HashSet<SubjectAssignment>();
            SubjectManagements = new HashSet<SubjectManagement>();
            ClassSubjectManagements = new HashSet<ClassSubjectManagement>();
            StudentClassResultSheets = new HashSet<StudentClassResultSheet>();
            Rellocations = new HashSet<Rellocation>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameDari { get; set; }

        public virtual ICollection<School> School { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
        public virtual ICollection<ScheduleExam> ScheduleExams { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }
        public virtual ICollection<SubjectManagement> SubjectManagements { get; set; }
        public virtual ICollection<ClassSubjectManagement> ClassSubjectManagements { get; set; }
        public virtual ICollection<StudentClassResultSheet> StudentClassResultSheets { get; set; }
        public virtual ICollection<Rellocation> Rellocations { get; set; }
    }
}
