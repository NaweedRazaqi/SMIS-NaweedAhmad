using App.Domain.Entity.look;
using Clean.Domain.Entity.doc;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class Teacher
    {
        public Teacher()
        {
            SubjectAssignments = new HashSet<SubjectAssignment>();
        
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? CreatedBy { get; set; }
        public int? GenderId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Salary { get; set; }
        public int? SarviceTypId { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public string NationalId { get; set; }
        public int? EthnicityId { get; set; }
        public string PhotoPath { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? OfficeId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Code { get; set; }
        public int? StatusId { get; set; }
        public string LastName { get; set; }

        public virtual Location BirthLocation { get; set; }
        public virtual Location DistrictNavigation { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Office Office { get; set; }
        public virtual Location ProvinceNavigation { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Service SarviceTyp { get; set; }
        public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; }

    }
}
