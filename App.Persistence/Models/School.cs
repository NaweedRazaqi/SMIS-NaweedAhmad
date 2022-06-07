using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class School
    {
        public School()
        {
            ClassManagement = new HashSet<ClassManagement>();
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short SchoolTypeId { get; set; }

        public virtual SchoolType SchoolType { get; set; }
        public virtual ICollection<ClassManagement> ClassManagement { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
