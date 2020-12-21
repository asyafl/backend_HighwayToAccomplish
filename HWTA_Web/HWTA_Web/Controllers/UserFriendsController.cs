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
    public class UserFriendsController : ControllerBase
    {
        private readonly IUserFriendsManager _userFriendsManager;

        public UserFriendsController(IUserFriendsManager userFriendsManager)
        {
            _userFriendsManager = userFriendsManager;
        }

        [HttpPost("/AddFriend")]
        public async Task<IActionResult> AddFriend(FriendRequest request)
        {
            var userId = User.ParseUserId();

            var result = await _userFriendsManager.AddFriendAsync(userId, request.FriendId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("/deleteFriend")]
        public async Task<IActionResult> DeleteFriend(FriendRequest request)
        {
            var userId = User.ParseUserId();

            var result = await _userFriendsManager.DeleteFriendAsync(userId, request.FriendId);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/getAllUserFriends")]
        public async Task<IActionResult> GetAllUserFriends()
        {
            var userId = User.ParseUserId();

            var result = await _userFriendsManager.GetAllFriendsAsync(userId);

            if(result.Count() == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
