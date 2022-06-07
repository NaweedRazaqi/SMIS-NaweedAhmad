using App.Domain.Entity.look;
using Clean.Domain.Entity.doc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity.prf
{
    public class StudentClassResultSheet
    {
        public int Id { get; set; }
        public short SchoolTypeId { get; set; }
        public short StudentSchoolId { get; set; }
        public short ClassTypeId { get; set; }
        public int? DocumentTypeId { get; set; }
        public short ClassManagementId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public int? OwnerId { get; set; }
        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual SchoolType SchoolType { get; set; }
        public virtual School StudentSchool { get; set; }
    }
}
