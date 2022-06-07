using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Lookup.Models
{
    public class ModuleStatusModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int? Sorter { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveName { get; set; }
    }
}
