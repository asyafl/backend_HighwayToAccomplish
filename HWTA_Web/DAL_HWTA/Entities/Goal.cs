using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL_HWTA.Entities
{
    public class Goal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GoalType GoalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Regularity { get; set; }

        public int Value { get; set; }

        public string ValueType { get; set; }

        public bool IsActive { get; set; }

        [InverseProperty(nameof(UserGoal.Goal))]
        public List<UserGoal> UserGoals { get; set; } 
    }
}
