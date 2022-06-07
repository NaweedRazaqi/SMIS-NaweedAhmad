using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Subject.Models
{
    public class SearchSubjectModel
    {
        public long? Id { get; set; }
        public String Name { get; set; }
        public String NameEng{ get; set; }
        public String Remarks { get; set; }
        public int? ViewOrder { get; set; }
        public int? StatusId { get; set; }
        public String StatusName { get; set; }
        public short? SchoolTypeId { get; set; }
        public string SchoolTypeName { get; set; }
    }
}
