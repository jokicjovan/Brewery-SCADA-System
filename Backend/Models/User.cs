namespace Brewery_SCADA_System.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<AnalogInput> AnalogInputs { get; set; }
        public List<DigitalInput> DigitalInputs { get; set; }

        public User(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
            AnalogInputs = new List<AnalogInput>();
            DigitalInputs = new List<DigitalInput>();
        }

        public User()
        {
            AnalogInputs = new List<AnalogInput>();
            DigitalInputs = new List<DigitalInput>();
        }
    }
}
