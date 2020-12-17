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

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int Regularity { get; set; }

        public int Value { get; set; }

        public int Progress { get; set; }



        [ForeignKey(nameof(UserId))]
        public User User {get;set;}


        [ForeignKey(nameof(GoalId))]
        public Goal Goal { get; set; }


    }
}
