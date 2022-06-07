using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public class SchoolCategory
    {
        public SchoolCategory()
        {
            StudentRegistration = new HashSet<StudentRegisteration>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
    }
}
