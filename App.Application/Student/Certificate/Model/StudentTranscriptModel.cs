using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Student.Certificate.Model
{
  public  class StudentTranscriptModel
    {

        public long? ProfileId { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public long? _10studentclasid { get; set; }
        public long? _10thsubject { get; set; }
        public long? _10classMark { get; set; }
        public long? _11studentclassid { get; set; }
        public long? _11subjects { get; set; }
        public string StudentSubjects { get; set; }
        public long? _11classMark { get; set; }
        public long? _12studetnclassid { get; set; }
        public long? _12subjects { get; set; }
        public long? _12classMark { get; set; }
    }
}
