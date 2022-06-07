using System;
using System.Collections.Generic;

namespace Clean.UI.ssModel
{
    public partial class ScreenDocument
    {
        public int Id { get; set; }
        public short? ScreenId { get; set; }
        public short? DocumentTypeId { get; set; }
        public bool? IsActive { get; set; }
    }
}
