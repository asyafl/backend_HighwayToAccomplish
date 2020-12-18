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
                                            string description, DateTime startDate, DateTime endDate, int regularity, int value, string valueType )
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
                StartDate = startDate,
                EndDate = endDate,
                Regularity = regularity,
                Value = value,
                ValueType = valueType
            };


                _dbContext.Goals.Add(newGoal);

            var isActive = true;
            if(startDate > DateTime.Now)
            {
                isActive = false;
            }

            var userGoal = new UserGoal
            {
                UserId = userId,
                Goal = newGoal,
                IsActive = isActive,
                Progress = 0,
                LastUpdateDate = DateTime.Now,
                IsCompleted = false

            };

            _dbContext.UserGoals.Add(userGoal);
                
                await _dbContext.SaveChangesAsync();
            }

    }
}
