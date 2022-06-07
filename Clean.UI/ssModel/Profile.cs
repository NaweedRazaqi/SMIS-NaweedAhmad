using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class Profile
    {
        public Profile()
        {
            Registeration = new HashSet<Registeration>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Registeration> Registeration { get; set; }
    }
}
