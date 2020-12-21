using DAL_HWTA.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL_HWTA
{
    public class HwtaDbContext : DbContext
    {
        public HwtaDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<GoalProgress> GoalProgresses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGoal> UserGoals { get; set; }

    }
}
