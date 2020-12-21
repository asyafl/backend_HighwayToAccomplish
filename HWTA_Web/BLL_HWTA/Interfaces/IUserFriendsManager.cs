using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
   public interface IUserFriendsManager
    {
        Task<bool> AddFriendAsync(long userId, long friendId);

        Task<bool> DeleteFriendAsync(long userId, long friendId);

        Task<List<AllUsersModel>> GetAllFriendsAsync(long userId);
    }
}
