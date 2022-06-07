using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
   public class StudentHealthReport
    {

        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
