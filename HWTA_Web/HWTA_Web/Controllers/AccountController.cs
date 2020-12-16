using BLL_HWTA.Models;
using HWTA_Web.TokenApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private static List<User> people = new List<User>
        {
            new User {Email="admin@gmail.com", Password="12345", Role = "admin" },
            new User {Email="qwerty@gmail.com", Password="55555", Role = "user" }
        };

        [HttpPost("/auth")]
        public IActionResult Auth(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var jwt = GetToken(identity);

            var response = new
            {
                access_token = jwt,
                username = identity.Name
            };

            return Json(response);
        }


        [HttpPost("/registration")]
        public IActionResult Registration(string username, string password)
        {
            var newUser = AddUser(username, password);
            if (newUser)
            {
                var identity = GetIdentity(username, password);

                var jwt = GetToken(identity);

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



        private string GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private bool AddUser(string email, string password)
        {
            User user = people.FirstOrDefault(x => x.Email == email);

            if(user == null)
            {
                people.Add(new User { Email = email, Password = password, Role = "user" });
                return true;
            }

            return false;
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = people.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
