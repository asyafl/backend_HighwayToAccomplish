using System;
using System.Collections.Generic;
using System.Text;

namespace BLL_HWTA.Requests
{
    public class CreateUserGoalRequest
    {
        public string GoalTitle { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Regularity { get; set; }
        public int Value { get; set; }
        public string ValueType { get; set; }
    }
}
