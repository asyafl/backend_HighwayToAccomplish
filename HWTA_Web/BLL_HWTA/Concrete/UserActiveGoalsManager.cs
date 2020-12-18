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
   public class UserActiveGoalsManager : IUserActiveGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public UserActiveGoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserActiveGoalsModel>> GetAllActiveUserGoalsAsync(long userId)
        {
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such user");
            }

            var activeUserGoals = await _dbContext.UserGoals
                .Where(x => x.UserId == userId && x.IsCompleted == false && x.Goal.IsActive == true && x.Goal.GoalType == GoalType.Custom)
                .Select(x => new {
                    UserGoal = x,
                    Goal = x.Goal,
                    ActiveDates = x.GoalProgresses.Select(y => y.ActivityDate).OrderByDescending(x => x),
                    LastUpdateDate = x.GoalProgresses.Max(y => y.ActivityDate)
                })
                .ToArrayAsync();

            var result = new List<UserActiveGoalsModel>();

            foreach (var g in activeUserGoals)
            {
                int progress = 0;
                int i = 0;
                DateTime prevDate = DateTime.Now;
                foreach (var a in g.ActiveDates)
                {
                    if (i == 0)
                    {
                        progress++;
                    }
                    else if ((prevDate.Date - a.Date).Days > 1)
                    {
                        break;
                    }
                    else
                    {
                        progress++;
                    }

                    i++;
                    prevDate = a;
                }

                result.Add(new UserActiveGoalsModel
                {
                    NameGoal = g.Goal.Name,
                    Description = g.Goal.Description,
                    StartDate = g.Goal.StartDate,
                    EndDate = g.Goal.EndDate,
                    LastUpdateDate = g.LastUpdateDate,
                    Value = g.Goal.Value,
                    ValueType = g.Goal.ValueType,
                    Progress = progress
                });

            }

            return result;

        }
    }
}
