using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
   public interface IUserActiveGoalsManager
    {
        Task<List<UserActiveGoalsModel>> GetAllActiveUserGoalsAsync(long userId);
    }
}
