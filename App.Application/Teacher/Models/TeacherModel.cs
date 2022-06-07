using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Teacher.Models
{
   public  class TeacherModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
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
        public string LastName { get; set; }
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
        public string GenderText { get; set; }
        public string EthnicityText { get; set; }
        public string ProvinceText { get; set; }
        public string DistrictText { get; set; }
        public string NID { get; set; }
        public string DocumentTypeText { get; set; }
        public string BirthLocationName { get; set; }
        public string NIDText { get; set; }
        public string ServiceText { get; set; }
        public int? StatusId { get; set; }

        public string DobShamsi { get; set; }
    }
}
