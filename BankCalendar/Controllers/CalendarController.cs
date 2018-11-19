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
        public IEnumerable<CalendarItem> GetCalendarItem()
        {
            return _context.CalendarItem;
        }

        // GET: api/Calendar/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCalendarItem([FromRoute] int id)
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

            return NoContent();
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

            return CreatedAtAction("GetCalendarItem", new { id = calendarItem.ID }, calendarItem);
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

        //GET: api/Calendar/Date
        [Route("date")]
        [HttpGet]
        public async Task<List<string>> GetDate()
        {
            var calendar = (from m in _context.CalendarItem
                            select m.Date).Distinct();

            var returned = await calendar.ToListAsync();

            return returned;
        }
    }
}