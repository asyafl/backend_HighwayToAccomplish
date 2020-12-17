using BLL_HWTA.Interfaces;
using BLL_HWTA.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {

        private readonly IAccountManager _accountManager;
        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("/auth")]
        public async Task<IActionResult> Auth(AccountLoginRequest request)
        {
            var identity = await _accountManager.GetIdentityAsync(request.Email, request.Password );
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var jwt = _accountManager.GetToken(identity);

            var response = new
            {
                access_token = jwt,
                email = identity.Name
            };

            return Json(response);
        }


        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(AccountCreateRequest request)
        {
            var newUser = await _accountManager.AddUserAsync(request.Email, request.Password, request.FirstName, request.LastName);

            if (newUser)
            {
                var identity = await _accountManager.GetIdentityAsync(request.Email, request.Password);

                var jwt = _accountManager.GetToken(identity);

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
