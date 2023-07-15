using System.ComponentModel.DataAnnotations;

namespace Brewery_SCADA_System.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(40, ErrorMessage = "Email must be between 5 and 40 characters", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "Email is not in valid form.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password must be between 5 and 30 characters", MinimumLength = 5)]
        public string Password { get; set; }

        public UserLoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
