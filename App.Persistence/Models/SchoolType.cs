using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class SchoolType
    {
        public SchoolType()
        {
            School = new HashSet<School>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<School> School { get; set; }
    }
}
