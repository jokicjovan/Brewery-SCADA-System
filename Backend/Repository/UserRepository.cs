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
            return await _entities.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<User> FindByIdWithTags(Guid id)
        {
            return await _entities
                .Include(e => e.AnalogInputs).ThenInclude(e => e.Alarms)
                .Include(e => e.DigitalInputs)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
