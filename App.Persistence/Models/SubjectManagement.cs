using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class SubjectManagement
    {
        public SubjectManagement()
        {
            ClassSubjectManagement = new HashSet<ClassSubjectManagement>();
            HighSchoolStudentClassMarks = new HashSet<HighSchoolStudentClassMarks>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int? StatusId { get; set; }
        public int? ViewOrder { get; set; }
        public string Remarks { get; set; }
        public TimeSpan? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ICollection<ClassSubjectManagement> ClassSubjectManagement { get; set; }
        public virtual ICollection<HighSchoolStudentClassMarks> HighSchoolStudentClassMarks { get; set; }
    }
}
