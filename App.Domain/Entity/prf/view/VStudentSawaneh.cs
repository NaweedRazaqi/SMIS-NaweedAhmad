using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf.view
{
    public partial class VStudentSawaneh
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public short? BirthLocationId { get; set; }
        public short? MotherLanguageId { get; set; }
        public string PhotoPath { get; set; }
        public string FatherNameEng { get; set; }
        public string MotherLanguage { get; set; }
        public int? StudentAssassNumber { get; set; }
        public short? SchoolId { get; set; }
        public string SchoolName { get; set; }
        public short? ClassTypeId { get; set; }
        public string Classtype { get; set; }
        public string PermenentLocation { get; set; }
        public short? RelativeTypeId { get; set; }
        public string RelativeName { get; set; }
        public string RelativeType { get; set; }
        public short? ProfessionTypeId { get; set; }
        public string FatherProfession { get; set; }
        public string StudentHealth { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Reasons { get; set; }
        public int? Fine { get; set; }
        public string TdocumentNo { get; set; }
        public short? TerminatedClass { get; set; }
        public string TerminatedClassText { get; set; }
    }
}
