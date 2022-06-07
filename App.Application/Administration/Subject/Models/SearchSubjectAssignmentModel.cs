using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Administration.Subject.Models
{
   public class SearchSubjectAssignmentModel
    {

        public long Id { get; set; }
        public short? SchoolTypeId { get; set; }
        public short? SubjectId { get; set; }
        public long? ClassManagementId { get; set; }
        public int? ClassTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }
        public string ClasstypeIdText { get; set; }
        public string SubjectText { get; set; }
        public string ClassManagementText { get; set; }
        public string SchoolTypeName { get; set; }

    }
}
