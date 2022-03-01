using Events.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repositories
{
    public class EventRepository
    {
        private readonly EventsAPIContext db;

        public EventRepository(EventsAPIContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CodeEvent>> GetAsync(bool includeLectures)
        {
            return includeLectures ? await db.CodeEvent.Include(c => c.Location)
                                                        .Include(c => c.Lectures)
                                                        .ToListAsync() :
                                      await db.CodeEvent.Include(c => c.Location)
                                                        .ToListAsync();
        }
    }
}
