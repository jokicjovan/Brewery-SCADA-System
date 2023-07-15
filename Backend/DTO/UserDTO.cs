using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Brewery_SCADA_System.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(40, ErrorMessage = "Email must be between 5 and 40 characters", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "Email is not in valid form.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password must be between 5 and 30 characters", MinimumLength = 5)]
        public string Password { get; set; }

        public UserDTO(string name, string surname, string email, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
        }

        public UserDTO()
        {
            
        }
    }
}
