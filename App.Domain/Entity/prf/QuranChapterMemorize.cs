using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public  class QuranChapterMemorize
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Chapter { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
