using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
    public class ClassManagementModel
    {
        public long Id { get; set; }
        public int? ClassTypeId { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public int? SchoolId { get; set; }
        public int? Year { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
    }
}
