using System;
using System.Collections.Generic;
using App.Domain.Entity.prf;

namespace App.Domain.Entity.look
{
    public partial class School
    {
        public School()
        {
            ClassManagement = new HashSet<ClassManagement>();
           
            RellocationNewSchool = new HashSet<Rellocation>();
            RellocationOldSchool = new HashSet<Rellocation>();
            StudentRegistration = new HashSet<StudentRegisteration>();
            StudentClasses = new HashSet<StudentClass>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short SchoolTypeId { get; set; }
        public short? SchoolCategoryId { get; set; }

        public virtual SchoolCategory SchoolCategory { get; set; }
        public virtual SchoolType SchoolType { get; set; }
        public virtual ICollection<ClassManagement> ClassManagement { get; set; }
        public virtual ICollection<Rellocation> RellocationNewSchool { get; set; }
        public virtual ICollection<Rellocation> RellocationOldSchool { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}
