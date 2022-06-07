using App.Domain.Entity.prf;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Prf.Models
{
    public class StudentClassSubjectModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public int? Marks { get; set; }
        public long HSSCMId { get;set; }
    }
}
