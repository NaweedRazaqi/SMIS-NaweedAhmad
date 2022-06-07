using System;
using System.Collections.Generic;
using App.Domain.Entity.look;

namespace App.Domain.Entity.prf
{
    public partial class StudentClassResult
    {
        public long Id { get; set; }
        public long? StudentClassId { get; set; }
        public int? ResultId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual Result Result { get; set; }
        public virtual StudentClass StudentClass { get; set; }
    }
}
