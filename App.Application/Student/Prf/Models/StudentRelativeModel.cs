using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
   public class StudentRelativeModel
    {

        public int Id { get; set; }
        public long? ProfileId { get; set; }
        public string Name { get; set; }
        public int? RelativeTypeId { get; set; }
        public string RelativeTypeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
