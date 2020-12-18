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
    public class UserPlannedGoalsManager : IUserPlannedGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public UserPlannedGoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UserPlannedGoalsModel>> GetAllPlannedUserGoalsAsync(long userId)
        {
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such user");
            }

           return await _dbContext.UserGoals
                .Where(x => x.UserId == userId && x.IsCompleted == false && x.Goal.IsActive == false && x.Goal.GoalType == GoalType.Custom)
                .Select(x => new UserPlannedGoalsModel
                {
                    NameGoal = x.Goal.Name,
                    Description = x.Goal.Description,
                    StartDate = x.Goal.StartDate,
                    EndDate = x.Goal.EndDate,
                    Value = x.Goal.Value,
                    ValueType = x.Goal.ValueType
                })
                .ToListAsync();

           
            
        }
    }
}
