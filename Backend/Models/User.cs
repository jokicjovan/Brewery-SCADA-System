namespace Brewery_SCADA_System.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<AnalogInput> AnalogInputs { get; set; }
        public List<DigitalInput> DigitalInputs { get; set; }

        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public User()
        {
            
        }
    }
}
