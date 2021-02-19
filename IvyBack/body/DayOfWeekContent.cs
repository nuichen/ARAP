using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.body
{
    public class DayOfWeekContent
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<DaysOfContent> Contents { get; set; } = new List<DaysOfContent>();
        public string CustomerId { get; set; }
        public DateTime DateTime { get; set; }
    }

}
