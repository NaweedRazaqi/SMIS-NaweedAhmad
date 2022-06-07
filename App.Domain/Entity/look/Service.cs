using App.Domain.Entity.prf;
using System.Collections.Generic;

namespace App.Domain.Entity.look
{
    public class Service
    {


        public Service()
        {
           
            Teacher = new HashSet<Teacher>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string NameDari { get; set; }
        public string NamePashto { get; set; }

        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
