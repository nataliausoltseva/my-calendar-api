using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankCalendar.Models
{
    public class BankCalendarContext : DbContext
    {
        public BankCalendarContext (DbContextOptions<BankCalendarContext> options)
            : base(options)
        {
        }

        public DbSet<BankCalendar.Models.CalendarItem> CalendarItem { get; set; }
    }
}
