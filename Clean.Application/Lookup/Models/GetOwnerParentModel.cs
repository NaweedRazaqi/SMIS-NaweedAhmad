using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Lookup.Models
{
    public class GetOwnerParentModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
    }
}
