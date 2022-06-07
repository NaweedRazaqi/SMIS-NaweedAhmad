using System;
using System.Collections.Generic;

namespace App.Persistence.Models
{
    public partial class VProfileProcess
    {
        public long? Id { get; set; }
        public int? ServiceTypeId { get; set; }
        public long? ApplicationId { get; set; }
        public int? ProcessId { get; set; }
    }
}
