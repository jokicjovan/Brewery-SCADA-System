using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;

namespace Brewery_SCADA_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void createUser(UserDTO userDto) 
        {
            User user = new User();
            user.Username = userDto.Username;
            user.Password = userDto.Password;
            user.Role = "User";
            _userRepository.Create(user);
        }
    }
}
