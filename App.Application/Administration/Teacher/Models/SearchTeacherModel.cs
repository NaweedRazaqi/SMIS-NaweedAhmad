using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Teacher.Models
{
    public class SearchTeacherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
}
