using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.System.Models
{
   public class SearchOwnerModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public short? ProvinceId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ParentName { get; set; }
        public string IsActiveText { get; set; }
        public string ProvinceText { get; set; }
    }
}
