
using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf

{
    public partial class PrimarySecondaryResult
    {

        public decimal Id { get; set; }
        public long? StudentClassId { get; set; }
        public int? ResultId { get; set; }
        public string Pathfile { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual Result Result { get; set; }
        public virtual StudentClass StudentClass { get; set; }
    }
}
