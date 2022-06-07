using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Examination.Result.Models
{
    public class SearchStudentNewResultModel
    {

        public int Id { get; set; }
        public decimal? ProfileId { get; set; }
        public int ClassTypeId { get; set; }
        public long ClassManagementId { get; set; }
        public int? Total { get; set; }
        public int ResultId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ClassTypeText { get; set; }
        public string ClassManagementText { get; set; }
        public string ResultText { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string LasName { get; set; }
        public bool? IsActive { get; set; }


    }
}
