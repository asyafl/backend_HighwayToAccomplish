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
    public class UserCompletedGoalsController : ControllerBase
    {
        private readonly IUserCompletedGoalsManager _manager;

        public UserCompletedGoalsController(IUserCompletedGoalsManager manager)
        {
            _manager = manager;
        }

        [HttpGet("/getAllCompletedUserGoals")]
        public async Task<IActionResult> GetAllCompletedUserGoals()
        {
            var userId = User.ParseUserId();
            var model = await _manager.GetAllCompletedUserGoalsAsync(userId);

            return Ok(model);
        }
    }
}
