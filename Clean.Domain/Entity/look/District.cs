using Clean.Domain.Entity.look;
using System;
using System.Collections.Generic;

namespace Clean.Domain.Entity.look
{
    public partial class District
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
}
