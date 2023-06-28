namespace Brewery_SCADA_System.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public UserDTO()
        {
            
        }
    }
}
