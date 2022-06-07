using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
    public class StudentHealthReportModel
    {

        public int Id { get; set; }
        public decimal ProfileId { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
