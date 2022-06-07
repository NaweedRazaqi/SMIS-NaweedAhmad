using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;


namespace App.Domain.Entity.look
{
    public partial class Office
    {
        public Office()
        {
            Profile = new HashSet<Profile>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TitleEn { get; set; }
        public int OrganizationId { get; set; }
        public int OfficeTypeId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Location Province { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
