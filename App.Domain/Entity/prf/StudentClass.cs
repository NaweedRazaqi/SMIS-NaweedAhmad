
using App.Domain.Entity.look;
using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class StudentClass
    {

        public StudentClass()
        {
            StudentClassMarks = new HashSet<StudentClassMarks>();
            PrimarySecondaryResult = new HashSet<PrimarySecondaryResult>();
            StudentClassResult = new HashSet<StudentClassResult>();
        }

        public int? Id { get; set; }
        public int? ProfileId { get; set; }
        public short? ClassTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public short ClassManagementId { get; set; }
        public bool? IsActive { get; set; }
        public short? SchoolId { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<StudentClassMarks> StudentClassMarks { get; set; }
        public virtual ICollection<PrimarySecondaryResult> PrimarySecondaryResult { get; set; }
        public virtual ICollection<StudentClassResult> StudentClassResult { get; set; }
    }

}