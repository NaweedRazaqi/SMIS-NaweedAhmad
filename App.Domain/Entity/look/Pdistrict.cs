using App.Domain.Entity.prf;
using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.look
{
   public  class Pdistrict
    {
        public Pdistrict()
        {
            StudentRegistration = new HashSet<StudentRegisteration>();
        }

        public short Id { get; set; }
        public short? ProvinceId { get; set; }
        public string Name { get; set; }
        public int? DistrictCode { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
    }
}
