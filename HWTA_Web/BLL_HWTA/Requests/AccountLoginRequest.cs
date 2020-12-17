using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Requests
{
    public class AccountLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
