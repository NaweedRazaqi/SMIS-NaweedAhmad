using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Relocation.Models
{
   public class SearchRellocationModel
    {

        public long Id { get; set; }
        public decimal? ProfileId { get; set; }
        public short? SchoolTypeId { get; set; }
        public int? OldSchoolId { get; set; }
        public int? NewSchoolId { get; set; }
        public int? OldAssasNumber { get; set; }
        public int? NewAssasNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? SchoolLocationId { get; set; }
        public string NewSchoolNameText { get; set; }
        public string OldSchoolNameText { get; set; }
        public string SchoolLocationNameText { get; set; }
        public string ProfileName { get; set; }
        public int? District { get; set; }
        public string DistrictName { get; set; }
        

    }
}
