using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;
namespace App.Domain.Entity.look
{
    public  class Profession
    {
        public Profession()
        {
            Jobs = new HashSet<Jobs>();
            Relatives = new HashSet<Relatives>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
        public virtual ICollection<Relatives> Relatives { get; set; }
    }
}
