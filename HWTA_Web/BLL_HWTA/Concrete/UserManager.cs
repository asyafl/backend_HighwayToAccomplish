using BLL_HWTA.Interfaces;
using BLL_HWTA.Models;
using DAL_HWTA;
using DAL_HWTA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly HwtaDbContext _dbContext;

        private readonly HashSet<string> mimeTypes = new HashSet<string>() 
        { "image/gif", "image/jpeg", "image/pjpeg", "image/png", "image/svg+xml", 
            "image/tiff", "image/vnd.microsoft.icon", "image/vnd.wap.wbmp", "image/webp"};

        public UserManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddFriendAsync(long userId, long friendId)
        {
            if (userId == friendId)
            {
                throw new ArgumentException("Can't be the same id");
            }

            var users = await _dbContext.Users.Where(x => x.Id == userId || x.Id == friendId).ToListAsync();

            if (users.Count != 2)
            {
                throw new ArgumentException("No such user");
            }

            var existingFriend = await _dbContext.Friends.FirstOrDefaultAsync(x => 
                x.UserId == userId && x.FriendId == friendId 
                || x.FriendId == userId && x.UserId == friendId);

            if (existingFriend != null && existingFriend.FriendStatus == FriendStatus.Friend)
            {
                return false;
            }
            else if (existingFriend != null && existingFriend.FriendStatus == FriendStatus.Deleted)
            {
                existingFriend.FriendStatus = FriendStatus.Friend;

                await _dbContext.SaveChangesAsync();
                return true;

            }
            else if (existingFriend == null)
            {
                _dbContext.Friends.Add(new Friend 
                  { UserId = userId, 
                    FriendId = friendId,
                    FriendStatus = FriendStatus.Friend,
                    FriendshipStartDate = DateTime.Now
                });

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DownloadUserProfilePictureAsync(long userId, byte[] picture, string contentType)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user == null)
            {
                throw new ArgumentException("No such user");
            }
            else if (!mimeTypes.Contains(contentType))
            {
                return false;
            }
            else
            {
                user.ContentType = contentType;
                user.ProfilePicture = picture;

                await _dbContext.SaveChangesAsync();
                return true;
            }

        }

        public async Task<List<AllUsersModel>> GetAllUsersProfilesAsync(long userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("No such user");
            }

            return  await _dbContext.Users.Where(x => x.Id != userId)
                .Select(x => new AllUsersModel
                { 
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Picture = x.ContentType == null || x.ProfilePicture == null ? null 
                            : $"data:{x.ContentType};base64, " + Convert.ToBase64String(x.ProfilePicture),
                })
                .ToListAsync();

        }

        public async Task<UserProfileModel> GetUserProfileInfoAsync(long userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("No such user");
            }

           return new UserProfileModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

           
        }

        public async Task<UserFileModel> GetUserProfilePictureAsync(long userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("No such user");
            }

            return new UserFileModel
            {
                Picture = user.ProfilePicture,
                ContentType = user.ContentType
            };
        }
    }
}
