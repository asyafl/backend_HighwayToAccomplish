using DAL_HWTA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL_HWTA
{
   public class HwtaDbContext: DbContext
    {
        public HwtaDbContext(DbContextOptions options) : base(options){}
        public DbSet<Goal> Goals { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGoal> UserGoals { get; set; }

    }
}
