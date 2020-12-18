using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Models
{
   public class UserActiveGoalsModel
    {
        public string NameGoal { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int Value { get; set; }
        public string ValueType { get; set; }

    }
}
