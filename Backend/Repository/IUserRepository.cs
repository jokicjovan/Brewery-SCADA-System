using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
        public Task<User> FindByEmail(String email);
        public Task<User> FindByIdWithTags(Guid id);
    }
}