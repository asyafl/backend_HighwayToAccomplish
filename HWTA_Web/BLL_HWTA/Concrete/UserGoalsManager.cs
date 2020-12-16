using BLL_HWTA.Interfaces;
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
    public class UserGoalsManager : IUserGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public UserGoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateGoalAsync(long userId, long goalId)
        {
            var checkGoalId = await _dbContext.Goals.AnyAsync(x => x.Id == goalId);
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkGoalId)
            {
                throw new ArgumentException("No such goal id");
            }

            if (!checkUserId)
            {
                throw new ArgumentException("No such userId");
            }


            var checkGoal = await _dbContext.UserGoals
                .FirstOrDefaultAsync(x => x.GoalId == goalId && x.UserId == userId && x.IsActive == true);

            if (checkGoal != null)
            {
                return false;
            }
            else
            {
                _dbContext.UserGoals.Add(new UserGoal
                {
                    UserId = userId,
                    GoalId = goalId,
                    StartDate = DateTime.Now,
                    IsActive = true
                });

                await _dbContext.SaveChangesAsync();
                return true;
            }



        }
    }
}
