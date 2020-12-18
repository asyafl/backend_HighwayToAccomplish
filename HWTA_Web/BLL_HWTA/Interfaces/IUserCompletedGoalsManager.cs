using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IUserCompletedGoalsManager
    {
        Task<List<UserCompletedGoalsModel>> GetAllCompletedUserGoalsAsync(long userId);
    }
}
