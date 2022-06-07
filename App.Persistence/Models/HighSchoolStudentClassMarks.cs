using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class HighSchoolStudentClassMarks
    {
        public long Id { get; set; }
        public decimal? StudentClassId { get; set; }
        public long? SubjectId { get; set; }
        public long? Marks { get; set; }
        public bool? IsAbsent { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual StudentClass StudentClass { get; set; }
        public virtual SubjectManagement Subject { get; set; }
    }
}
