using App.Application.Lookup.Models;
using App.Domain.Entity.look;
using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
    public class SearchProfileModel
    {
        public string FatherName { get; set; }
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FatherNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public string? Email { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? DepartmentType { get; set; }
        public int? EthnicityId { get; set; }
        public int? ReligionId { get; set; }
        public string Comments { get; set; }
        public string Remark { get; set; }
        public int? BloodGroupId { get; set; }
        public string GenderText { get; set; }
        public string BloodGroupText { get; set; }
        public string EthnicityText { get; set; }
        public string BirthLocationText { get; set; }
        public string ReligionText { get; set; }
        public string MaritalStatusText { get; set; }
        public int Province { get; set; }
        public string ProvinceText { get; set; }
        public int District { get; set; }
        public string DistrictText { get; set; }
        public int? DocumentTypeId { get; set; }
        public string PhotoPath { get; set; }
        public string NID { get; set; }
        public string DocumentTypeText { get; set; }
        public string NIDText { get; set; }
        public string DoBText { get; set; }
        public int? MotherLanguageId { get; set; }
        public string MotherLanguageName { get; set; }
        // public string JobSalary { get; set; }
        public string TazkiraNumber { get; set; }
        public string Mobile { get; set; }
        public string CreatedOn { get; set; }
        public int? SchoolId { get; set; }
        public string? QuranChapter { get; set; }
        public string ClassTypeName { get; set; }
        public string SchoolName { get; set; }
        public int RegistrationTypeId { get; set; }
        public string RegistrationTypeName { get; set; }
        public int OrganizationId { get; set; }
        public int? PhysicalConditionID { get; set; }
        public string PhysicalConditionText { get; set; }
        public string IslamicEducationTypeText { get; set; }
        public string BirthLocationName { get; set; }
        public string EconomicSituationText { get; set; }
        public string Age { get; set; }
        public string YearOfBirth { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public string DobShamsi { get; set; }


    }
}
