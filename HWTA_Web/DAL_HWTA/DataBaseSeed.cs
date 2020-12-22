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
            if (!_dbContext.Goals.Any(x =>x.GoalType == GoalType.Global))
            {
                CreateGoals();
            }       
        }

        private void CreateGoals()
        {
            _dbContext.Goals.AddRange(new Goal[] {
            new Goal{
                Name = "Sport every day",
                Description = "Make any exercises every day till New Year",
                GoalType = GoalType.Global,
                CreationDate = new System.DateTime(2020, 12, 01), 
                EndDate = new System.DateTime(2020, 12, 31),
                IsActive = true
            }

            }); 

            _dbContext.SaveChanges();
        }
    }
}
