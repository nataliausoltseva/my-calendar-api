using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCalendar.Models
{
    public class CalendarItem
    {
        public int ID { get; set; }
        public string DateTime { get; set; }
        public string Event { get; set; }
        public string Location { get; set; }
    }
}
