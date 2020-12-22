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
   public class UserFriendsManager: IUserFriendsManager
    {
        private readonly HwtaDbContext _dbContext;

        public UserFriendsManager(HwtaDbContext dbContext)
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
                {
                    UserId = userId,
                    FriendId = friendId,
                    FriendStatus = FriendStatus.Friend,
                    FriendshipStartDate = DateTime.Now
                });

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteFriendAsync(long userId, long friendId)
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
                existingFriend.FriendStatus = FriendStatus.Deleted;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            else if (existingFriend != null && existingFriend.FriendStatus == FriendStatus.Deleted)
            {
                return false;

            }
            else if (existingFriend == null)
            {
                return false;
            }

            return false;
        }

        public async Task<List<AllUsersModel>> GetAllFriendsAsync(long userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("No such user");
            }


            var friendsIds = (await _dbContext.Friends
                .Where(x => (x.UserId == userId || x.FriendId == userId) && x.FriendStatus == FriendStatus.Friend)
                .ToArrayAsync())
                .SelectMany(x => new[] { x.FriendId, x.UserId })
                .Distinct()
                .Where(x => x != userId)
                .ToList();


          return await  _dbContext.Users.Where(x => friendsIds.Contains(x.Id))
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
    }
}
