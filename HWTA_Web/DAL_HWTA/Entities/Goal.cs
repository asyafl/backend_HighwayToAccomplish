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


        [InverseProperty(nameof(UserGoal.Goal))]
        public List<UserGoal> UserGoals { get; set; } 
    }
}
