using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.StudentUpgrade.Models
{
   public  class StudentUpgradeSearch
    {
        public int? Id { get; set; }
        public int? ProfileId { get; set; }
        public short? ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? ModifiedOn { get; set; }
    }
}
