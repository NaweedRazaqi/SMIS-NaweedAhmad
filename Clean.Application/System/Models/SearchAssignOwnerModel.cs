using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.System.Models
{
   public class SearchAssignOwnerModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OwnerId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string IsActiveText { get; set; }
        public string UserText { get; set; }
        public string OwnerText { get; set; }
    }
}
