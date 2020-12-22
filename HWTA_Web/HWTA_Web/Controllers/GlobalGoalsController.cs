using BLL_HWTA.Interfaces;
using BLL_HWTA.Requests;
using HWTA_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GlobalGoalsController : ControllerBase
    {
        private readonly IGlobalGoalsManager _globalGoalsManager;

        public GlobalGoalsController(IGlobalGoalsManager globalGoalsManager)
        {
            _globalGoalsManager = globalGoalsManager;
        }

        [HttpGet("/getAllGlobalGoals")]
        public async Task<IActionResult> GetAllGlobalGoals()
        {
            var result = await _globalGoalsManager.GetAllGlobalGoalsAsync();

            return Ok(result);
        }

        [HttpPost("/takePartInGlobalGoal")]
        public async Task<IActionResult> TakePartInGlobalGoal(GoalRequest request)
        {
            var userId = User.ParseUserId();

            var result = await _globalGoalsManager.TakePartInGlobalGoalsAsync(userId, request.GoalId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
