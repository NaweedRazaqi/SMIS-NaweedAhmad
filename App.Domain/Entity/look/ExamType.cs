using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
    public partial class ExamType
    {
        public ExamType()
        {
            StudentClassMarks = new HashSet<StudentClassMarks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Pashto { get; set; }
        public string Dari { get; set; }

        public virtual ICollection<StudentClassMarks> StudentClassMarks { get; set; }
    }
}
