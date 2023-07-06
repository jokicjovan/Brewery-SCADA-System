using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Services
{
    public interface IUserService
    {
        public Task<User> Authenticate(User user);
        public Task<User> CreateUser(User user);
        Task<User> Get(Guid id);
    }
}
