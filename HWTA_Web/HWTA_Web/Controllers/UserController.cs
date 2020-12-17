using BLL_HWTA.Interfaces;
using HWTA_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HWTA_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/GetProfilePicture")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var userId = User.ParseUserId();
            var result = await _userManager.GetUserProfilePictureAsync(userId);

            return File(result.Picture, result.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> LoadProfilePicture(IFormFile uploadedFile)
        {
            var userId = User.ParseUserId();
            byte[] picture;

            var contentType = uploadedFile.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memoryStream);
                picture =  memoryStream.ToArray();
            }

            await _userManager.DownloadUserProfilePictureAsync(userId, picture, contentType);

            return Ok();
        }

        [HttpGet("/GetUserProfileInfo")]
        public async Task<IActionResult> GetUserProfileInfo()
        {
            var userId = User.ParseUserId();
            var model = await _userManager.GetUserProfileInfoAsync(userId);

            return Ok(model);
        }

    }
}
