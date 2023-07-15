using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }

        public async Task<User> FindByEmail(String email)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.FirstOrDefaultAsync(e => e.Email == email);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<User> FindByIdWithTags(Guid id)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities
                    .Include(e => e.AnalogInputs).ThenInclude(e => e.Alarms)
                    .Include(e => e.DigitalInputs)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            finally
            {
                Global._semaphore.Release();
            }
        }

        public async Task<List<User>> GetAllByCreatedBy(Guid userId)
        {
            await Global._semaphore.WaitAsync();

            try
            {
                await Task.Delay(1);
                return await _entities.Where(e => e.CreatedBy.Id == userId).ToListAsync();
            }
            finally
            {
                Global._semaphore.Release();
            }
        }
    }
}
