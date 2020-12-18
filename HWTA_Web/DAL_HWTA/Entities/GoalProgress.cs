using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL_HWTA.Entities
{
   public class GoalProgress
    {
        public long Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public long UserGoalId { get; set; }


        [ForeignKey(nameof(UserGoalId))]
        public UserGoal UserGoal { get; set; }
    }
}
