using App.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
   
     public class ClassUpgradation
    {
        public int? Id { get; set; }
        public int? ProfileId { get; set; }
        public short? ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? ModifiedOn { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
    }
}
