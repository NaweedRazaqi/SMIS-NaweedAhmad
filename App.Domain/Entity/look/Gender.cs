using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public partial class Gender
    {
        public Gender()
        {
            Profiles = new HashSet<Profile>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }



    }
}
