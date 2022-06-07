using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class Registeration
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int CaseId { get; set; }
        public int? UnitId { get; set; }
        public int? StatusId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Modifiedby { get; set; }

        public virtual Case Case { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Status Status { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
