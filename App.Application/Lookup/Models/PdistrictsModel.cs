using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
   public class PdistrictsModel
    {


        public short Id { get; set; }
        public short? ProvinceId { get; set; }
        public string Name { get; set; }
        public int? DistrictCode { get; set; }
    }
}
