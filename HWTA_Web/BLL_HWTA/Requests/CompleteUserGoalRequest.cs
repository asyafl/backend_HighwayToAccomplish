using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Requests
{
   public class CompleteUserGoalRequest
    {
        public long GoalId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
