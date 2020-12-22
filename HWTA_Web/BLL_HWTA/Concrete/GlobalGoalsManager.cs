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
   public class GlobalGoalsManager: IGlobalGoalsManager
    {
        private readonly HwtaDbContext _dbContext;

        public GlobalGoalsManager(HwtaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GlobalGoalsModel>> GetAllGlobalGoalsAsync()
        {
            return await _dbContext.Goals
                .Where(x => x.IsActive == true && x.GoalType == GoalType.Global)
                .Select(x => new GlobalGoalsModel 
                { 
                   GoalId =  x.Id, 
                   NameGoal = x.Name,
                   Description = x.Description, 
                   StartDate = x.CreationDate, 
                   EndDate = x.EndDate, 
                   IsActive = x.IsActive, 
                   Value = x.Value, 
                   ValueType = x.ValueType
                })
                .ToListAsync();
        }
    }
}
