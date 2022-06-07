using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class Profile
    {
        public Profile()
        {
            StudentClass = new HashSet<StudentClass>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Prefix { get; set; }
        public int? Suffix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BirthLocationId { get; set; }
        public int GenderId { get; set; }
        public int MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public int DocumentTypeId { get; set; }
        public string NationalId { get; set; }
        public int? EthnicityId { get; set; }
        public int? BloodGroupId { get; set; }
        public int StatusId { get; set; }
        public int? EducationTypeId { get; set; }
        public string PhotoPath { get; set; }
        public int? RegistrationType { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public string Mobile { get; set; }
        public int? Cprovince { get; set; }
        public int? Cdistrict { get; set; }
        public string Cvillage { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public int? ClassTypeId { get; set; }
        public int? SchoolId { get; set; }
        public int? OrganizationId { get; set; }

        public virtual Location BirthLocation { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Location CdistrictNavigation { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual Location CprovinceNavigation { get; set; }
        public virtual Location DistrictNavigation { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Education EducationType { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Location ProvinceNavigation { get; set; }
        public virtual Relation Religion { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }
    }
}
