using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class Result
    {
        public Result()
        {
            PrimarySecondaryResult = new HashSet<PrimarySecondaryResult>();
            StudentClassResult = new HashSet<StudentClassResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DariName { get; set; }
        public string PashtoName { get; set; }

        public virtual ICollection<PrimarySecondaryResult> PrimarySecondaryResult { get; set; }
        public virtual ICollection<StudentClassResult> StudentClassResult { get; set; }
    }
}
