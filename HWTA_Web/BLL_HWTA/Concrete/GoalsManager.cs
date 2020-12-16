using BLL_HWTA.Interfaces;
using BLL_HWTA.Models;
using DAL_HWTA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Concrete
{
    public class GoalsManager : IGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public GoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GoalModel>> GetAllGoalsAsync()
        {
            return await _dbContext.Goals
                .Select(x => new GoalModel { Id = x.Id, Name = x.Name}).ToListAsync();
        }
    }
}
