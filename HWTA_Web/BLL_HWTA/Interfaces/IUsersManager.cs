using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfases
{
   public interface IUsersManager
    {
        // Task<UserLoginModel> LoginAsync(string email, string password);
        Task<ClaimsIdentity> GetIdentityAsync(string email, string password);

        string GetToken(ClaimsIdentity identity);

        Task<bool> AddUserAsync(string email, string password, string firstName, string lastName);
    }
}
