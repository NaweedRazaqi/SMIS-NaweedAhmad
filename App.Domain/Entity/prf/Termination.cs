using App.Domain.Entity.look;
using Clean.Domain.Entity.doc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public class Termination
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public short? ClassTypeId { get; set; }
        public string Reasons { get; set; }
        public int? Fine { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DocumentNo { get; set; }
        public int? DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ClassType ClassType { get; set; }
    }
}
