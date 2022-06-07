using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Registration.Model
{
   public class StudentRegisterationModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public short SchoolTypeId { get; set; }
        public short SchoolId { get; set; }
        public short? SchoolCategoryId { get; set; }
        public short ClassTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public short? ProvinceId { get; set; }
        public short? PdistrictId { get; set; }
        public DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public string ClassTypeText { get; set; }
        public string ClassmanagementName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolTypeName { get; set; }
        public string ProvinceText { get; set; }
        public string DistrictText { get; set; }
        public string SchoolCattext { get; set; }
        
    }
}
