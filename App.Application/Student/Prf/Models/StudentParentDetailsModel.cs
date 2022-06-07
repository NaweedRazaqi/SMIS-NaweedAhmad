using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
   public class StudentParentDetailsModel
    {
        public int Id { get; set; }
        public decimal ProfileId { get; set; }
        public int? RelativeTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? JobLocationId { get; set; }
        public string GurrenterName { get; set; }
        public string GurrenterFatherName { get; set; }
        public int? ProfessionTypeId { get; set; }
        public string Phone { get; set; }
        public string RelativeName { get; set; }
        public string JobLocationName { get; set; }
        public string ProfesionName { get; set; }
        public string RelativeTypeText { get; set; }

    }
}
