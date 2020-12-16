using BLL_HWTA.Interfases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IUsersManager _userManager;
        public AccountController(IUsersManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("/auth")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            var identity = await _userManager.GetIdentityAsync(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var jwt = _userManager.GetToken(identity);

            var response = new
            {
                access_token = jwt,
                email = identity.Name
            };

            return Json(response);
        }


        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(string email, string password, string firstName, string lastName)
        {
            var newUser = await _userManager.AddUserAsync(email, password, firstName, lastName);

            if (newUser)
            {
                var identity = await _userManager.GetIdentityAsync(email, password);

                var jwt = _userManager.GetToken(identity);

                var response = new
                {
                    access_token = jwt,
                    username = identity.Name
                };

                return Json(response);
            }
            else
            {
                return BadRequest(new { errorText = "Thу user with this e-mail is already registered" });
            }
        }

    }
}
