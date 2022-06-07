using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
  public class RelativesType
    {
        public RelativesType()
        {
            Relatives = new HashSet<Relatives>();
            Jobs = new HashSet<Jobs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameDari { get; set; }
        public string NamePashto { get; set; }

        public virtual ICollection<Relatives> Relatives { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
     }
}
