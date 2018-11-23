using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BankCalendar.Models;
using BankCalendar.Controllers;

namespace UnitTestCalendarApi
{
    [TestClass]
    public class PutUnitTests
    {
        public static readonly DbContextOptions<BankCalendarContext> options = new DbContextOptionsBuilder<BankCalendarContext>().UseInMemoryDatabase(databaseName: "testDatabase").Options;
        public static readonly IList<string> calendarEvents = new List<string> { "party", "interview" };

        [TestInitialize]
        public void SetupDb()
        {
            using (var context = new BankCalendarContext(options))
            {
                CalendarItem calendaritem1 = new CalendarItem()
                {
                    Event = calendarEvents[0]
                };

                CalendarItem calendaritem2 = new CalendarItem()
                {
                    Event = calendarEvents[1]
                };

                context.CalendarItem.Add(calendaritem1);
                context.CalendarItem.Add(calendaritem2);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new BankCalendarContext(options))
            {
                context.CalendarItem.RemoveRange(context.CalendarItem);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestPutCalendarItemUpdate()
        {
            using (var context = new BankCalendarContext(options))
            {
                // Given
                string title = "putEvent";
                CalendarItem calendaritem1 = context.CalendarItem.Where(x => x.Event == calendarEvents[0]).Single();
                calendaritem1.Event = title;

                // When
                CalendarController calendarController = new CalendarController(context);
                IActionResult result = await calendarController.PutCalendarItem(calendaritem1.ID, calendaritem1) as IActionResult;

                // Then
                calendaritem1 = context.CalendarItem.Where(x => x.Event == title).Single();
            }
        }

        [TestMethod]
        public async Task TestGetCalendarItem()
        {
            using (var context = new BankCalendarContext(options))
            {
                // Given
                CalendarItem calendaritem1 = context.CalendarItem.First();

                // When
                CalendarController calendarController = new CalendarController(context);
                IActionResult result = await calendarController.GetCalendarItem(calendaritem1.ID) as IActionResult;

                // Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CalendarItem;
                Assert.IsNotNull(model);
                Assert.AreEqual(calendaritem1.Event, model.Event);
            }
        }

        [TestMethod]
        public async Task TestGetCalendarItems()
        {
            using (var context = new BankCalendarContext(options))
            {
                // When
                CalendarController calendarController = new CalendarController(context);
                IActionResult result = await calendarController.GetCalendarItems() as IActionResult;

                // Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as IEnumerable<CalendarItem>;
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());
            }
        }

        [TestMethod]
        public async Task TestPostCalendarItem()
        {
            using (var context = new BankCalendarContext(options))
            {
                // Given
                CalendarItem calendaritem1 = new CalendarItem { Event = "interview", Location = "Auckland", Start = new System.DateTime(2018, 11, 24, 22, 10, 25), End = new System.DateTime(2018, 11, 24) };

                // When
                CalendarController calendarController = new CalendarController(context);
                IActionResult result = await calendarController.PostCalendarItem(calendaritem1) as IActionResult;

                // Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CalendarItem;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.ID);
                Assert.AreEqual(calendaritem1.Event, model.Event);
                Assert.AreEqual(calendaritem1.Location, model.Location);
                Assert.AreEqual(calendaritem1.Start, model.Start);
                Assert.AreEqual(calendaritem1.End, model.End);

                Assert.AreEqual(true, context.CalendarItem.Any(x => x.ID == model.ID));
            }
        }

        [TestMethod]
        public async Task TestDeleteCalendarItem()
        {
            using (var context = new BankCalendarContext(options))
            {
                // Given
                CalendarItem calendaritem1 = context.CalendarItem.First();

                // When
                CalendarController calendarController = new CalendarController(context);
                IActionResult result = await calendarController.DeleteCalendarItem(calendaritem1.ID) as IActionResult;

                // Then
                var okObjectResult = result as OkObjectResult;
                Assert.IsNotNull(okObjectResult);

                var model = okObjectResult.Value as CalendarItem;
                Assert.IsNotNull(model);
                Assert.AreNotEqual(0, model.ID);
                Assert.AreEqual(calendaritem1.Event, model.Event);
                Assert.AreEqual(calendaritem1.Location, model.Location);
                Assert.AreEqual(calendaritem1.Start, model.Start);
                Assert.AreEqual(calendaritem1.End, model.End);

                Assert.AreEqual(false,context.CalendarItem.Any(x => x.ID == calendaritem1.ID));
            }
        }


    }
}