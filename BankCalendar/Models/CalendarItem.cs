using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCalendar.Models
{
    public class CalendarItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Event { get; set; }
        public string Location { get; set; }
        public string Starts { get; set; }
        public string Ends { get; set; }
        public string Day { get; set; }
    }
}
