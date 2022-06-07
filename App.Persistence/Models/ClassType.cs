using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class ClassType
    {
        public ClassType()
        {
            ClassManagement = new HashSet<ClassManagement>();
            Profile = new HashSet<Profile>();
            StudentClass = new HashSet<StudentClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DariName { get; set; }
        public string PashtoName { get; set; }

        public virtual ICollection<ClassManagement> ClassManagement { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }
    }
}
