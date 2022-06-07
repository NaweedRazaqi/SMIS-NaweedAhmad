using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public class BloodGroup
    {
        public BloodGroup()
        {
            Profile = new HashSet<Profile>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profile> Profile { get; set; }
    }
}
