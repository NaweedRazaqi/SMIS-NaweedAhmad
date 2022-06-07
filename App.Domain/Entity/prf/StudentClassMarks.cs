using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class StudentClassMarks
    {
        public long Id { get; set; }
        public int? StudentClassId { get; set; }
        public short? SubjectId { get; set; }
        public int Marks { get; set; }
        public bool? IsAbsent { get; set; }
        public int? ExamTypeId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ExamType ExamType { get; set; }
        public virtual StudentClass StudentClass { get; set; }
        public virtual SubjectManagement Subject { get; set; }

    }
}
