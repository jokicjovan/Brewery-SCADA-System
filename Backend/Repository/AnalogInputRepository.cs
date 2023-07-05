using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class AnalogInputRepository : CrudRepository<AnalogInput>, IAnalogInputRepository
    {
        public AnalogInputRepository(DatabaseContext context) : base(context) { }

        public async Task<AnalogInput> FindByIdWithAlarmsAndUsers(Guid id)
        {
            return await _entities.Include(e => e.Alarms).Include(u => u.Users).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task DeleteAlarms(Guid tagId)
        {
            var tag = await _context.AnalogInput.Include(t => t.Alarms).FirstOrDefaultAsync(t => t.Id == tagId);
            if (tag != null)
            {
                tag.Alarms.Clear();
                await _context.SaveChangesAsync();
            }
        }
    }
}
