﻿using BLL_HWTA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
   public interface IGlobalGoalsManager
    {
        Task<List<GlobalGoalsModel>> GetAllGlobalGoalsAsync();

        Task<bool> TakePartInGlobalGoalsAsync(long userId, long goalId);
    }
}
