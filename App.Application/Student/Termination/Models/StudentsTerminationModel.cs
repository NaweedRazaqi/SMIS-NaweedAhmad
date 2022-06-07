using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
    public class StudentsTerminationModel
    {
        public int Id { get; set; }
        public decimal ProfileId { get; set; }
        public short? ClassTypeId { get; set; }
        public string Reasons { get; set; }
        public int? Fine { get; set; }
        public int? DocumentTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DocumentNo { get; set; }
        
        public string TerminationDateShamsi { get; set; }
        public string ClassTypeText { get; set; }
    }
}
