using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Requests
{
   public class AccountCreateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
