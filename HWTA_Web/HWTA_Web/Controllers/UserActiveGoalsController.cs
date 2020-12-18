using BLL_HWTA.Interfaces;
using HWTA_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserActiveGoalsController : ControllerBase
    {
        private readonly IUserActiveGoalsManager _manager;

        public UserActiveGoalsController(IUserActiveGoalsManager manager)
        {
            _manager = manager;
        }

        [HttpGet("/getAllActiveUserGoals")]
        public async Task<IActionResult> GetAllActiveUserGoals()
        {
            var userId = User.ParseUserId();
            var model = await _manager.GetAllActiveUserGoalsAsync(userId);

            return Ok(model);
        }
    }
}
