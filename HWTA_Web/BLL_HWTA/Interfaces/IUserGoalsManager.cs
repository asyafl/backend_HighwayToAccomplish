using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IUserGoalsManager
    {
        Task CreateUserGoalAsync(long userId, string title,string description, DateTime startDate, DateTime endDate, int regularity, int value, string valueType);

        Task<List<UserActiveGoalsModel>> GetAllActiveUserGoalsAsync(long userId);

        Task<List<UserCompletedGoalsModel>> GetAllCompletedUserGoalsAsync(long userId);
    }
}
