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
                        DateTime = "Monday, 19-11-2018, From 7:00 PM to 11:30 PM",
                        Event = "My Birthday Party",
                        Location = "Auckland"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
