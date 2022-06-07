
using App.Domain.Entity.look;
using Clean.Domain.Entity.doc;
using System;
using System.Collections.Generic;
namespace App.Domain.Entity.prf
{
    public partial class Profile
    {
        public Profile()
        {
            StudentClass = new HashSet<StudentClass>();
            Relatives = new HashSet<Relatives>();
            Termination = new HashSet<Termination>();
            StudentHealthReports = new HashSet<StudentHealthReport>();
            Rellocations = new HashSet<Rellocation>();
            StudentRegistration = new HashSet<StudentRegisteration>();
            StudentClassResults = new HashSet<StudentResult>();
          

        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Prefix { get; set; }
        public int? Suffix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public int Key { get; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public int MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public int DocumentTypeId { get; set; }
        public string NationalId { get; set; }
        public int? EthnicityId { get; set; }
        public short? BloodGroupId { get; set; }
        public int StatusId { get; set; }
        public string PhotoPath { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public string Mobile { get; set; }
        public int? CProvince { get; set; }
        public int? CDistrict { get; set; }
        public string CVillage { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Email { get; set; }
        public int? OfficeId { get; set; }
        public int? MotherLanguageId { get; set; }
        public Location BirthLocation { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public virtual Location DistrictNavigation { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Languages Languages { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public virtual Location ProvinceNavigation { get; set; }
        public Religion Religion { get; set; }
        public ICollection<Application> Applications { get; set; }
        public virtual Office Office { get; set; }
        public virtual ICollection<StudentClass> StudentClass { get; set; }

        public virtual ICollection<Relatives> Relatives { get; set; }

        public virtual ICollection<StudentHealthReport> StudentHealthReports { get; set; }
        public virtual ICollection<Termination> Termination { get; set; }
        public virtual ICollection<Rellocation> Rellocations { get; set; }
        public virtual ICollection<StudentRegisteration> StudentRegistration { get; set; }
        public virtual ICollection<StudentResult> StudentClassResults { get; set; }
      


       

    }

}
