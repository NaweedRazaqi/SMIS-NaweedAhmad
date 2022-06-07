
using App.Domain.Entity.look;
using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Rellocation
    {

        public long Id { get; set; }
        public int? ProfileId { get; set; }
        public short? SchoolTypeId { get; set; }
        public short? OldSchoolId { get; set; }
        public short? NewSchoolId { get; set; }
        public int? OldAssasNumber { get; set; }
        public int? NewAssasNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? SchoolLocationId { get; set; }
        public int? District { get; set; }
        public virtual School NewSchool { get; set; }
        public virtual School OldSchool { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Location SchoolLocation { get; set; }
        public virtual Location DistrictNavigation { get; set; }
        public virtual SchoolType SchoolType { get; set; }

    }

}