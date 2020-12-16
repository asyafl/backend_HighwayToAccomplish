using BLL_HWTA.Interfaces;
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
    public class GoalController : ControllerBase
    {
        private readonly IGoalsManager _goalsManager;

        public GoalController(IGoalsManager goalsManager)
        {
            _goalsManager = goalsManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetListGoals()
        {
            var result = await _goalsManager.GetAllGoalsAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
