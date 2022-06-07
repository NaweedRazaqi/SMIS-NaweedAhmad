
using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class ClassSubjectManagement
    {
        public long Id { get; set; }
        public short? SubjectId { get; set; }
        public short? ClassTypeId { get; set; }
        public short? SchoolTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public short CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ReferenceNo { get; set; }

        public virtual ClassType ClassType { get; set; }
        public virtual SubjectManagement Subject { get; set; }
        public virtual SchoolType SchoolType { get; set; }


    }
}
