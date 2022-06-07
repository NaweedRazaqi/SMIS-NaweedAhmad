using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.Result.Models
{
   public class SearchStudentClassModel
    {

        public long Id { get; set; }
        public long? ProfileId { get; set; }
        public int? ClassTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public long? ClassManagementId { get; set; }
        public bool? IsActive { get; set; }
    }
}
