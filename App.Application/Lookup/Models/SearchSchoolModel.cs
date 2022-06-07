using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Lookup.Models
{
  public  class SearchSchoolModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Dari { get; set; }
        public string Pashto { get; set; }
        public string Code { get; set; }
        public short StatusId { get; set; }
        public short? SchoolTypeId { get; set; }
        public short? SchoolCategoryId { get; set; }
    }
}
