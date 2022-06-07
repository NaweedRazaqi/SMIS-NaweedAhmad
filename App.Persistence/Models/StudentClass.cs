using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class StudentClass
    {
        public StudentClass()
        {
            HighSchoolStudentClassMarks = new HashSet<HighSchoolStudentClassMarks>();
            PrimarySecondaryResult = new HashSet<PrimarySecondaryResult>();
            StudentClassResult = new HashSet<StudentClassResult>();
        }

        public long Id { get; set; }
        public long? ProfileId { get; set; }
        public int? ClassTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public long? ClassManagementId { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<HighSchoolStudentClassMarks> HighSchoolStudentClassMarks { get; set; }
        public virtual ICollection<PrimarySecondaryResult> PrimarySecondaryResult { get; set; }
        public virtual ICollection<StudentClassResult> StudentClassResult { get; set; }
    }
}
