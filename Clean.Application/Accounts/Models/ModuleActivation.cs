using System;
using System.Collections.Generic;
using System.Text;
using Clean.Application.Accounts.Models;

namespace Clean.Application.Accounts.Models
{
   public class ModuleActivation
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int? Sorter { get; set; }
        public Boolean IsActive { get; set; }
        public string IsActiveName { get; set; }
    }
}
