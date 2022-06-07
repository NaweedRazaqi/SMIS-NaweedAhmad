using App.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.Models
{
   public class StudentClassResultSheetModel
    {

        public int Id { get; set; }
        public short? SchoolTypeId { get; set; }
        public int StudentSchoolId { get; set; }
        public int ClassTypeId { get; set; }
        public long ClassManagementId { get; set; }
        public int DocumentTypeId { get; set; }
        public string StudentSchoolName { get; set; }
        public string ClassTypeName { get; set; }
        public string ClassManagementName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentTypeName { get; set; }
       public string SchoolTypeName { get; set; }
        public int? OwnerID { get; set; }
    }
}
