using BLL_HWTA.Interfaces;
using BLL_HWTA.Models;
using DAL_HWTA;
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
        public UserManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DownloadUserProfilePictureAsync(long userId, byte[] picture, string contentType)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if(user == null)
            {
                throw new ArgumentException("No such userId");
            }
            else
            {

                user.ContentType = contentType;
                user.ProfilePicture = picture;

                await _dbContext.SaveChangesAsync();
            }

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
