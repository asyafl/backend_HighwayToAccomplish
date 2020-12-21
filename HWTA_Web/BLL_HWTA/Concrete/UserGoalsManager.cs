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
    public class UserGoalsManager : IUserGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public UserGoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CompleteUserGoalAsync(long userId, long goalId, bool isCompleted)
        {
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such user");
            }

            var checkGoal = await _dbContext.UserGoals
                .FirstOrDefaultAsync(x => x.User.Id == userId && x.Goal.Id == goalId && x.IsCompleted == false);

            if(checkGoal == null)
            {
                throw new ArgumentException("No such goal");
            }
            else
            {
                checkGoal.IsCompleted = true;
                checkGoal.FinishDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task CreateUserGoalAsync(long userId, string title,
                                            string description, DateTime startDate, DateTime endDate, int regularity, int value, string valueType )
        {
         
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such user");
            }

            var isActive = true;
            if (startDate > DateTime.Now)
            {
                isActive = false;
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
                ValueType = valueType,
                IsActive = isActive
            };


                _dbContext.Goals.Add(newGoal);

           

            var userGoal = new UserGoal
            {
                UserId = userId,
                Goal = newGoal,
                FinishDate = null,
                IsCompleted = false, 
                DeletedDate = null,
                IsDeleted = false
            };

            _dbContext.UserGoals.Add(userGoal);
                
                await _dbContext.SaveChangesAsync();
            }

        public async Task<bool> DeletedUserGoalAsync(long userId, long goalId)
        {
            var checkUserGoal = await _dbContext.UserGoals
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GoalId == goalId && x.IsDeleted == false);

            if (checkUserGoal == null)
            {
                return false;
            }
            else
            {
                checkUserGoal.IsDeleted = true;
                checkUserGoal.DeletedDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<UserGoalsModel>> GetAllUserGoalsAsync(long userId)
        {
            var checkUserId = await _dbContext.Users.AnyAsync(x => x.Id == userId);

            if (!checkUserId)
            {
                throw new ArgumentException("No such user");
            }

            var userGoals = (await _dbContext.UserGoals
                .Where(x => x.UserId == userId  && x.Goal.GoalType == GoalType.Custom && x.IsDeleted == false)
                .Select(x => new {
                    UserGoal = x,
                    Goal = x.Goal,
                    ActiveDates = x.GoalProgresses.Select(y => y.ActivityDate),
                    LastUpdateDate = x.GoalProgresses.Max(y => y.ActivityDate),
                    CountActiveDates = x.GoalProgresses.Count()
                })
                .ToArrayAsync())
                .Select(x => new 
                {
                    UserGoal = x.UserGoal,
                    Goal = x.Goal,
                    ActiveDates = x.ActiveDates.OrderByDescending(ad => ad).ToArray(),
                    LastUpdateDate = x.LastUpdateDate,
                    CountActiveDates = x.CountActiveDates,
                });


            var result = new List<UserGoalsModel>();

            foreach (var g in userGoals)
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

                result.Add(new UserGoalsModel
                {
                    GoalId = g.Goal.Id,
                    NameGoal = g.Goal.Name,
                    Description = g.Goal.Description,
                    StartDate = g.Goal.StartDate,
                    PlannedEndDate = g.Goal.EndDate,
                    FinishedDate = g.UserGoal.FinishDate,
                    LastUpdateDate = g.LastUpdateDate,
                    Value = g.Goal.Value,
                    ValueType = g.Goal.ValueType,
                    Progress = progress,
                    CountActiveDays = g.CountActiveDates,
                    IsCompleted = g.UserGoal.IsCompleted,
                    IsActive = g.Goal.IsActive
                });

            }

            return result;

        }

        public async Task<bool> SubmitUserGoalAsync(long userId, long goalId)
        {
            var check = await _dbContext.UserGoals
                .Include(x => x.GoalProgresses)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GoalId == goalId 
                && x.IsCompleted == false && x.Goal.IsActive == true);

            if (check == null)
            {
                throw new ArgumentException("No such user with goal");
            }

            var t = check.GoalProgresses.Count;

            

            if (check.GoalProgresses.Count == 0 || check.GoalProgresses.Max(y => y.ActivityDate).Date != DateTime.Now.Date)
            {
                check.GoalProgresses.Add(new GoalProgress { ActivityDate = DateTime.Now });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }


        }
    }
    
}
