using BLL_HWTA.Interfaces;
using BLL_HWTA.Requests;
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
    public class UserGoalController : ControllerBase
    {
        private readonly IUserGoalsManager _userGoalsManager;

        public UserGoalController(IUserGoalsManager userGoalsManager)
        {
            _userGoalsManager = userGoalsManager;
        }

        [HttpPost("/addNewUserGoal")]
        public async Task<IActionResult> AddNewUserGoal(CreateUserGoalRequest request)
        {
            var userId = User.ParseUserId();
            await _userGoalsManager.CreateUserGoalAsync(userId, request.GoalTitle,
                request.Description, request.StartDate,  request.EndDate, request.Regularity, request.Value, request.ValueType);

            return Ok();

        }
    }
}
