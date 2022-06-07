using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Relocation.Models
{
   public class SearchStudentModel
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
        public int? GenderId { get; set; }
        public string GenderText { get; set; }
        public int Province { get; set; }
        public string ProvinceText { get; set; }
        public int District { get; set; }
        public string DistrictText { get; set; }
        public string PhotoPath { get; set; }
        public string NIDText { get; set; }
        public string DoBText { get; set; }
        public int? MotherLanguageId { get; set; }
        public string MotherLanguageName { get; set; }
        public string TazkiraNumber { get; set; }
        public string CreatedOn { get; set; }
        public string? QuranChapter { get; set; }
        public string ClassTypeName { get; set; }
        public string SchoolName { get; set; }
        public int RegistrationTypeId { get; set; }
        public string RegistrationTypeName { get; set; }
        public int OrganizationId { get; set; }
        public string BirthLocationName { get; set; }
        public string Age { get; set; }
        public string DobShamsi { get; set; }
        public string NID { get; set; }
        public string DocumentTypeText { get; set; }
        public string ClassName { get; set; }
        public int? StudentAssasnumber { get; set; }
        public string ClassManagement { get; set; }
        public int? DocumentTypeId { get; set; }
        public int SchoolId { get; set; }
        public long ClassTypeId { get; set; }
    }
}
