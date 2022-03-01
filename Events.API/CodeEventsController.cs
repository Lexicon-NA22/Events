#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Events.Core.Entities;
using Events.Data;

namespace Events.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly EventsAPIContext _context;

        public CodeEventsController(EventsAPIContext context)
        {
            _context = context;
        }

        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> GetCodeEvent()
        {
            return await _context.CodeEvent.ToListAsync();
        }

        // GET: api/CodeEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeEvent>> GetCodeEvent(Guid id)
        {
            var codeEvent = await _context.CodeEvent.FindAsync(id);

            if (codeEvent == null)
            {
                return NotFound();
            }

            return codeEvent;
        }

        // PUT: api/CodeEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeEvent(Guid id, CodeEvent codeEvent)
        {
            if (id != codeEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(codeEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeEventExists(id))
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

        // POST: api/CodeEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CodeEvent>> PostCodeEvent(CodeEvent codeEvent)
        {
            _context.CodeEvent.Add(codeEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCodeEvent", new { id = codeEvent.Id }, codeEvent);
        }

        // DELETE: api/CodeEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeEvent(Guid id)
        {
            var codeEvent = await _context.CodeEvent.FindAsync(id);
            if (codeEvent == null)
            {
                return NotFound();
            }

            _context.CodeEvent.Remove(codeEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CodeEventExists(Guid id)
        {
            return _context.CodeEvent.Any(e => e.Id == id);
        }
    }
}
