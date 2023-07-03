using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Services
{
    public interface IUserService
    {
        public Task<User> Authenticate(UserDTO userDTO);
        public void CreateUser(UserDTO userDTO);
    }
}
