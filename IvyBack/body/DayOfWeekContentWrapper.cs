using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IvyBack.body
{
    public class DayOfWeekContentWrapper
    {
        public DaysOfContent Content { get; set; }

        public RectangleF Rectangle { get; set; }

        public bool IsSelected { get; set; }

        public bool IsEmpty { get; set; }

        public string CustomerId { get; set; }

        public DateTime DateTime { get; set; }

        public DayOfWeekContent DayOfWeekContent { get; set; }
    }
}
