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
using Events.Data.Repositories;
using AutoMapper;
using Events.Core.Dtos;

namespace Events.API
{
    [Route("api/events")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly EventsAPIContext _context;
        private readonly IMapper mapper;
        private readonly EventRepository eventRepo;

        public CodeEventsController(EventsAPIContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
            eventRepo = new EventRepository(_context);
        }

        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEventDto>>> GetCodeEvent(bool includeLectures)
        {
            var events = await eventRepo.GetAsync(includeLectures);
            return Ok(mapper.Map<IEnumerable<CodeEventDto>>(events));
        }

        // GET: api/CodeEvents/5
        [HttpGet("{name}")]
        public async Task<ActionResult<CodeEventDto>> GetCodeEvent(string name, bool includeLectures)
        {

            if(string.IsNullOrEmpty(name))  return BadRequest();

            var codeEvent = await eventRepo.GetAsync(name, includeLectures);

            if (codeEvent == null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeEvent);

            return Ok(dto);
        }

        // PUT: api/CodeEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCodeEvent(Guid id, CodeEvent codeEvent)
        //{
        //    if (id != codeEvent.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(codeEvent).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CodeEventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/CodeEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CodeEventDto>> PostCodeEvent(CreateEventDto dto)
        {
            if(await eventRepo.GetAsync(dto.Name, false) != null)
            {
                ModelState.AddModelError("Name", "Name exists");
                return BadRequest();   
            }

            var codeEvent = mapper.Map<CodeEvent>(dto);
            await eventRepo.AddAsync(codeEvent);

            await _context.SaveChangesAsync();


            var model = mapper.Map<CodeEventDto>(codeEvent);
            return CreatedAtAction(nameof(GetCodeEvent), new { name = codeEvent.Name }, model);
        }

        //// DELETE: api/CodeEvents/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCodeEvent(Guid id)
        //{
        //    var codeEvent = await _context.CodeEvent.FindAsync(id);
        //    if (codeEvent == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CodeEvent.Remove(codeEvent);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CodeEventExists(Guid id)
        //{
        //    return _context.CodeEvent.Any(e => e.Id == id);
        //}
    }
}
