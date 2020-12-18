using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Models
{
    public class UserPlannedGoalsModel
    {
        public string NameGoal { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Value { get; set; }
        public string ValueType { get; set; }
    }
}
