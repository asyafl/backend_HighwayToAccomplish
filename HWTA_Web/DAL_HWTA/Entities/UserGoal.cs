using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL_HWTA.Entities
{
    public class UserGoal
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GoalId { get; set; }
        public DateTime StartDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public User User {get;set;}


        [ForeignKey(nameof(GoalId))]
        public Goal Goal { get; set; }


        [InverseProperty(nameof(DailyUserGoal.UserGoal))]
        public List<DailyUserGoal> DailyUserGoals { get; set; }
    }
}
