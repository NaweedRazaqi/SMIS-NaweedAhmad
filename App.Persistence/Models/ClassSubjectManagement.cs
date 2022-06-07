using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class ClassSubjectManagement
    {
        public long Id { get; set; }
        public long? SubjectId { get; set; }
        public long? ClassManagementId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual SubjectManagement Subject { get; set; }
    }
}
