using App.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace App.Domain.Entity.prf
{
    public partial class SubjectAssignment
    {
        public int Id { get; set; }
        public short SchoolTypeId { get; set; }
        public int? TeacherId { get; set; }
        public int? YearId { get; set; }
        public short? SubjectManagementId { get; set; }
        public short? ClassTypeId { get; set; }
        public short? ClassManagementId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ClassManagement ClassManagement { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual SchoolType SchoolType { get; set; }
        public virtual SubjectManagement SubjectManagement { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Year Year { get; set; }
    }
}
