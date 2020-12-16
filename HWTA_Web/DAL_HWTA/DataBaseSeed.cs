using DAL_HWTA.Entities;
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
            if (!_dbContext.UserGoals.Any())
            {
                CreateGoals();
            }        }

        private void CreateGoals()
        {
            _dbContext.Goals.AddRange(new Goal[] {
            new Goal{ Name = "Lose Weight" },
            new Goal{ Name = "Quit Smoking" },
            new Goal{ Name = "Exercise Regularly"},
            new Goal{ Name = "Maintain Weight" },
            new Goal{ Name = "Race" },

            });

            _dbContext.SaveChanges();
        }
    }
}
