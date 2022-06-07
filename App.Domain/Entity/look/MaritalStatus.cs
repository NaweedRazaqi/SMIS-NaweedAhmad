using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Profiles = new HashSet<Profile>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
