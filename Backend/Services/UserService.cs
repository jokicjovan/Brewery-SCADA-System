using Brewery_SCADA_System.Exceptions;
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

        public async Task<User> Authenticate(User user) {
            User authenticatedUser = await _userRepository.FindByEmail(user.Email);
            if (authenticatedUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, authenticatedUser.Password))
            {
                throw new ResourceNotFoundException("Email or password is incorrect!");
            }
            return authenticatedUser;
        }

        public async Task<User> CreateUser(User user) 
        {
            if (await _userRepository.FindByEmail(user.Email) != null)
            {
                throw new InvalidInputException("User with that email already exists!");
            }
            User createdUser = new User();
            createdUser.Name = user.Name;
            createdUser.Surname = user.Surname;
            createdUser.Email = user.Email;
            createdUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            createdUser.Role = user.Role;
            createdUser.CreatedBy= user.CreatedBy;
            createdUser.AnalogInputs = user.AnalogInputs;
            createdUser.DigitalInputs = user.DigitalInputs;
            return _userRepository.Create(createdUser);
        }
        public async Task <User> Get(Guid id)
        {
            User user = await _userRepository.FindByIdWithTags(id);
            return user == null ? throw new ResourceNotFoundException("User does not exist") : user;
        }
    }
}
