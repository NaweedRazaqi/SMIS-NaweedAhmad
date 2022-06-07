using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Application.Lookup.Models
{
   public class SearchUserNameModel
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int OfficeId { get; set; }
    }
}
