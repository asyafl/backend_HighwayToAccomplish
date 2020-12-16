using DAL_HWTA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Models
{
   public class UserLoginModel
    {
        public long UserId { get; set; }

        public Role Role { get; set; }
    }
}
