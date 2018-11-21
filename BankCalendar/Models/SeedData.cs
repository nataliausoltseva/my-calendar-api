using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCalendar.Models
{
    public class SeedData
    {
        private BankCalendarContext _context;

        public SeedData(BankCalendarContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        { 
            if (_context.CalendarItem.Count() > 0)
            {
                return;
            }

            _context.CalendarItem.AddRange(
                new CalendarItem
                {
                    Event = "My Birthday Party",
                    Location = "Auckland",
                    Start = new DateTime (2018,11,21,12,30,0,0),
                    End = new DateTime (2018,11,21,21,0,0)
                    }
            );
            _context.SaveChanges();
        }
    }
}
