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
        public async Task CreateUserGoalAsync(long userId, string title,
                                            string description, DateTime endDate, int regularity, int value )
        {
         
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such userId");
            }

            var newGoal = new Goal
            {
                Name = title,
                Description = description,
                GoalType = GoalType.Custom,
                EndDate = endDate,
                Regularity = regularity
            };


                _dbContext.Goals.Add(newGoal);

            var userGoal = new UserGoal 
            { 
                UserId = userId,
                Goal = newGoal,
                StartDate = DateTime.Now,
                IsActive = true,
                Progress = 0,
                LastUpdateDate = DateTime.Now,
                Value = value
            };

            _dbContext.UserGoals.Add(userGoal);
                
                await _dbContext.SaveChangesAsync();
            }

    }
}
