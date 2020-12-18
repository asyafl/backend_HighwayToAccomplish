using BLL_HWTA.Interfaces;
using HWTA_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserPlannedGoalsController : ControllerBase
    {
        private readonly IUserPlannedGoalsManager _manager;

        public UserPlannedGoalsController(IUserPlannedGoalsManager manager)
        {
            _manager = manager;
        }

        [HttpGet("/getAllPlannedUserGoals")]
        public async Task<IActionResult> GetAllPlannedUserGoals()
        {
            var userId = User.ParseUserId();
            var model = await _manager.GetAllPlannedUserGoalsAsync(userId);

            return Ok(model);
        }
    }
}
