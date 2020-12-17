using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IAccountManager
    {
        Task<ClaimsIdentity> GetIdentityAsync(string email, string password);

        string GetToken(ClaimsIdentity identity);

        Task<bool> AddUserAsync(string email, string password, string firstName, string lastName);
    }
}
