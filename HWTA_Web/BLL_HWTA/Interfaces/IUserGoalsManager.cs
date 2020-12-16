using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IUserGoalsManager
    {
        Task<bool> CreateGoalAsync(long userId, long goalId);
    }
}
