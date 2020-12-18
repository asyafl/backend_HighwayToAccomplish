﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Interfaces
{
    public interface IUserGoalsManager
    {
        Task CreateUserGoalAsync(long userId, string title,string description, DateTime startDate, DateTime endDate, int regularity, int value, string valueType);
    
    }
}
