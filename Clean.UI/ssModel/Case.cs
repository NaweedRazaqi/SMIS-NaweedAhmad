using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class Case
    {
        public Case()
        {
            Registeration = new HashSet<Registeration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProcessTime { get; set; }

        public virtual ICollection<Registeration> Registeration { get; set; }
    }
}
