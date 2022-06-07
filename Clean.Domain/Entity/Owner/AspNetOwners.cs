using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Domain.Entity.Owner
{
    public class AspNetOwners
    {
        public AspNetOwners()
        {
            InverseParent = new HashSet<AspNetOwners>();
            UserOwner = new HashSet<UserOwner>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public short? ProvinceId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual AspNetOwners Parent { get; set; }
        public virtual ICollection<AspNetOwners> InverseParent { get; set; }
        public virtual ICollection<UserOwner> UserOwner { get; set; }
        public virtual Province Province { get; set; }
     
    }
}
