using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf.view
{
   

    public  class StudentResultSheet
    {
        public long? Id { get; set; }
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
        public DateTime? DateOfBirth { get; set; }
        public int? BirthLocationId { get; set; }
        public int? GenderId { get; set; }
        public string NationalId { get; set; }
        public int? EthnicityId { get; set; }
        public string PhotoPath { get; set; }
        public string Mobile { get; set; }
        public int? MotherLanguageId { get; set; }
        public long? ProfileId { get; set; }
        public short? SchoolTypeId { get; set; }
        public int? SchoolId { get; set; }
        public int? ClassTypeId { get; set; }
        public int? StudentAssassNumber { get; set; }
        public long? ClassManagementId { get; set; }
        public string StudentSchoolName { get; set; }
        public string StudentClassName { get; set; }
        public string StudentSchoolType { get; set; }
        public string StudentClassManagement { get; set; }
        public string SchoolNameEng { get; set; }
    }
}
