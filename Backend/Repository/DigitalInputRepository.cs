using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class DigitalInputRepository : CrudRepository<DigitalInput>, IDigitalInputRepository
    {
        public DigitalInputRepository(DatabaseContext context) : base(context) { }

        public async Task<DigitalInput> FindByIdWithUsers(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.Include(u => u.Users).FirstOrDefaultAsync(e => e.Id == id);
            }
            finally
            {
                Global._semaphore.Release();
            }
            
        }

    }
}
