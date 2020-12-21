using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Models
{
   public class UserGoalsModel
    {
        public long GoalId { get; set; }
        public string NameGoal { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public int CountActiveDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public int Value { get; set; }
        public string ValueType { get; set; }
    }
}
