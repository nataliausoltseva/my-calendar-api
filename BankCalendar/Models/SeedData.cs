using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankCalendar.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BankCalendarContext(
                serviceProvider.GetRequiredService<DbContextOptions<BankCalendarContext>>()))
            {
                if (context.CalendarItem.Count() > 0)
                {
                    return;
                }

                context.CalendarItem.AddRange(
                    new CalendarItem
                    {
                        Date = "19-11-2018",
                        Event = "My Birthday Party",
                        Location = "Auckland",
                        Starts = "7:00 PM",
                        Ends = "11:30 PM",
                        Day = "Monday"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
