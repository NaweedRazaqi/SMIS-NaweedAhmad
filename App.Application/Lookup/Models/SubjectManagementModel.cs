using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class SubjectManagementModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public short? SchoolTypeId { get; set; }
    }
}
