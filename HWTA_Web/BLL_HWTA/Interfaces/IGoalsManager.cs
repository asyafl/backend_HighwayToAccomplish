using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
   public interface IGoalsManager
    {
        Task<List<GoalModel>> GetAllGoalsAsync();
    }
}
