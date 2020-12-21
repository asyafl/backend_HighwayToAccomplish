using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IUserManager
    {
        Task<UserFileModel> GetUserProfilePictureAsync(long userId);

        Task<bool> DownloadUserProfilePictureAsync(long userId, byte[] picture, string contentType);

        Task<UserProfileModel> GetUserProfileInfoAsync (long userId);

        Task <List<AllUsersModel>> GetAllUsersProfilesAsync(long userId);

       // Task<bool> AddFriend(long userId, );
    }
}
