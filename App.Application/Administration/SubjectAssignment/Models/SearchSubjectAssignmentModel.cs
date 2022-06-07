using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.SubjectAssignment.Models
{
    public class SearchSubjectAssignmentModel
    {
        public int Id { get; set; }
        public int? TeacherId { get; set; }
        public short? SchoolTypeId { get; set; }
        public string TeacherName { get; set; }
        public int? YearId { get; set; }
        public string YearName { get; set; }
        public long? SubjectManagementId { get; set; }
        public string SubjectManagementName { get; set; }
        public int? ClassTypeId { get; set; }
        public string ClassTypeName { get; set; }
        public long? ClassManagementId { get; set; }
        public string ClassManagementName { get; set; }
        public string SchoolTypeText { get; set; }
    }
}
