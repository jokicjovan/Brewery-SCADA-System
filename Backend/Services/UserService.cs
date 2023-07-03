using Brewery_SCADA_System.DTO;
using Brewery_SCADA_System.Exceptions;
using Brewery_SCADA_System.Models;
using Brewery_SCADA_System.Repository;
using System.Net;

namespace Brewery_SCADA_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(UserDTO userDTO) {
            User user = await _userRepository.FindByEmail(userDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
            {
                throw new ResourceNotFoundException("Email or password is incorrect!");
            }
            return user;
        }

        public void CreateUser(UserDTO userDTO) 
        {
            User user = new User();
            user.Email = userDTO.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            user.Role = "User";
            _userRepository.Create(user);
        }
    }
}
