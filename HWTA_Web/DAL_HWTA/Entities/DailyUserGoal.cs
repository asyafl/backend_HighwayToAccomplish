using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL_HWTA.Entities
{
   public class DailyUserGoal
    {
        public long Id { get; set; }
        public DateTime CheckDate { get; set; }
        public bool CheckActivity { get; set; }
        public long UserGoalId { get; set; }

        [ForeignKey(nameof(UserGoalId))]
        public UserGoal UserGoal { get; set; }
    }
}
