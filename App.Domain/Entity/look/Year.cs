using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public partial class Year
    {
        public Year()
        {
            SubjectAssignments = new HashSet<SubjectAssignment>();
        }
        public int Id { get; set; }
        public string Dari { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }
    }
}
