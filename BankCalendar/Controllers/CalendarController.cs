using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankCalendar.Models;

namespace BankCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly BankCalendarContext _context;

        public CalendarController(BankCalendarContext context)
        {
            _context = context;
        }

        // GET: api/Calendar
        [HttpGet]
        public async Task<IActionResult> GetCalendarItems()
        {
            var calendarItems = await _context.CalendarItem.ToListAsync();
            return Ok(calendarItems);
        }

        // GET: api/Calendar/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCalendarItem([FromRoute] int id)
        {
            var calendarItem = await _context.CalendarItem.FindAsync(id);
            if (calendarItem == null)
            {
                return NotFound();
            }
            return Ok(calendarItem);
        }

        // PUT: api/Calendar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarItem([FromRoute] int id, [FromBody] CalendarItem calendarItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calendarItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(calendarItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(calendarItem);
        }

        // POST: api/Calendar
        [HttpPost]
        public async Task<IActionResult> PostCalendarItem([FromBody] CalendarItem calendarItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CalendarItem.Add(calendarItem);
            await _context.SaveChangesAsync();

            return Ok(calendarItem);
        }

        public Task<IActionResult> PutEvent(object id, CalendarItem calendaritem1)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Calendar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calendarItem = await _context.CalendarItem.FindAsync(id);
            if (calendarItem == null)
            {
                return NotFound();
            }

            _context.CalendarItem.Remove(calendarItem);
            await _context.SaveChangesAsync();

            return Ok(calendarItem);
        }

        private bool CalendarItemExists(int id)
        {
            return _context.CalendarItem.Any(e => e.ID == id);
        }
    }
}