using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class ClassManagement
    {
        public ClassManagement()
        {
            ClassSubjectManagement = new HashSet<ClassSubjectManagement>();
            StudentClass = new HashSet<StudentClass>();
        }

        public long Id { get; set; }
        public int? ClassTypeId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int? SchoolId { get; set; }
        public int? Year { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ClassType ClassType { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<ClassSubjectManagement> ClassSubjectManagement { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }
    }
}
