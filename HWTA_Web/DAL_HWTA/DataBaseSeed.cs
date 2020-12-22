using DAL_HWTA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL_HWTA
{
    public class DataBaseSeed
    {
        private readonly HwtaDbContext _dbContext;
        public DataBaseSeed(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Goals.Any(x =>x.GoalType == GoalType.Global))
            {
                CreateGoals();
                CreateUser();
            }       
        }

        private void CreateGoals()
        {
            _dbContext.Goals.Add(
            new Goal{
                Name = "Stopped eating mayonnaise - pumped up a great press",
                Description = "The winter holidays are over - it's time to get your body in shape",
                GoalType = GoalType.Global,
                CreationDate = new System.DateTime(2020, 12, 01), 
                EndDate = new System.DateTime(2020, 12, 31),
                IsActive = true

            }); 

            _dbContext.SaveChanges();
        }

        private void CreateUser()
        {
            var user = new User
            {

                FirstName = "Keanu",
                LastName = "Reeves",
                Email = "customer@gmail.com",
                PasswordHash = new byte[] { 55, 54, 235, 40, 138, 207, 5, 251, 115, 39, 81, 63, 245, 208, 156, 207, 113, 171, 182, 72, 217, 212, 115, 218, 218, 12, 65, 165, 38, 97, 33, 41, 60, 87, 231, 174, 75, 69, 179, 86, 106, 13, 96, 153, 26, 255, 151, 13, 10, 47, 163, 158, 28, 93, 89, 72, 177, 116, 37, 210, 73, 74, 113, 145 },
                Role = Role.User,
            };

            var goal = new Goal
            {
                Name = "Read every day",
                Description = "Read every day",
                GoalType = GoalType.Custom,
                CreationDate = new System.DateTime(2020, 12, 01),
                EndDate = new System.DateTime(2020, 12, 27),
                Regularity = 7,
                Value = 5,
                ValueType = "pages",
                IsActive = true
            };



            _dbContext.Users.Add(user);

            _dbContext.Goals.Add(goal);

            var userGoal = new UserGoal
            {
                User = user,
                Goal = goal,
                IsCompleted = false,
                IsDeleted = false,
                StartDate = new System.DateTime(2020, 12, 01)
            };

            _dbContext.UserGoals.Add(userGoal);

            var progresses = new List<GoalProgress>();

            for(int i = 1; i < 22; i++ )
            {
                var date = new System.DateTime(2020, 12, i);
                progresses.Add(new GoalProgress {
                    UserGoal = userGoal,
                    ActivityDate = date
                });
            };

            _dbContext.GoalProgresses.AddRange(progresses);
            _dbContext.SaveChanges();
        }
    }
}
