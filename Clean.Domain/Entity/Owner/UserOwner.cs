using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Domain.Entity.Owner
{
    public class UserOwner
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual AspNetOwners Owner { get; set; }
      //  public virtual User User { get; set; }
    }
}
