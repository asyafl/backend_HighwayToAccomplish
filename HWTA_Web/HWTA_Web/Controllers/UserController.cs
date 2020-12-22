using BLL_HWTA.Interfaces;
using HWTA_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
            var file = await _userManager.GetUserProfilePictureAsync(userId);

            if (file.ContentType == null || file.Picture == null)
            {
                return NotFound();
            }
            else
            {
                var strResult = $"data:{file.ContentType};base64, " + Convert.ToBase64String(file.Picture);
                return Ok(strResult);
            }


        }

        [HttpPost("/LoadProfilePicture")]
        public async Task<IActionResult> LoadProfilePicture(IFormFile uploadedFile)
        {
            var userId = User.ParseUserId();
            byte[] picture;

            var contentType = uploadedFile.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memoryStream);
                picture = memoryStream.ToArray();
            }

            var result = await _userManager.DownloadUserProfilePictureAsync(userId, picture, contentType);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("/GetUserProfileInfo")]
        public async Task<IActionResult> GetUserProfileInfo()
        {
            var userId = User.ParseUserId();
            var model = await _userManager.GetUserProfileInfoAsync(userId);

            return Ok(model);
        }

        [HttpGet("/getAllUsersProfiles")]
        public async Task<IActionResult> GetAllUsersProfiles()
        {
            var userId = User.ParseUserId();

            var model = await _userManager.GetAllUsersProfilesAsync(userId);

                return Ok(model);
            
        }


    }
}
