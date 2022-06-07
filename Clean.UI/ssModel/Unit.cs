using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class Unit
    {
        public Unit()
        {
            Registeration = new HashSet<Registeration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Registeration> Registeration { get; set; }
    }
}
