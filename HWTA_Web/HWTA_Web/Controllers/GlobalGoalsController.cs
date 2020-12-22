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
    }
}
