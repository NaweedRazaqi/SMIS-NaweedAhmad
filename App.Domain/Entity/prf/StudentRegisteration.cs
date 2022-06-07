using App.Domain.Entity.look;
using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public  class StudentRegisteration
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public short SchoolId { get; set; }
        public short SchoolTypeId { get; set; }
        public short? SchoolCategoryId { get; set; }
        public short ClassTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public short? ProvinceId { get; set; }
        public short? PdistrictsId { get; set; }
        public DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        //public  Pdistricts Pdistrict { get; set; }
        public Pdistrict Pdistricts { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Province Province { get; set; }
        public virtual School School { get; set; }
        public virtual SchoolCategory SchoolCategory { get; set; }
        public virtual SchoolType SchoolType { get; set; }
    }
}
