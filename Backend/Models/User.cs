namespace Brewery_SCADA_System.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<AnalogInput> AnalogInputs { get; set; }
        public List<DigitalInput> DigitalInputs { get; set; }

        public User? CreatedBy { get; set; }

        public User(string name, string surname, string email, string password, string role, User createdBy)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Role = role;
            if (createdBy != null)
            {
                AnalogInputs = createdBy.AnalogInputs;
                DigitalInputs = createdBy.DigitalInputs;
            }
            else
            {
                AnalogInputs = new List<AnalogInput>();
                DigitalInputs = new List<DigitalInput>();
            }

            CreatedBy = createdBy;
        }

        public User(Guid id,string name, string surname, string email, string password, string role, User createdBy)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Role = role;
            if (createdBy != null)
            {
                AnalogInputs = createdBy.AnalogInputs;
                DigitalInputs = createdBy.DigitalInputs;
            }
            else
            {
                AnalogInputs = new List<AnalogInput>();
                DigitalInputs = new List<DigitalInput>();
            }

            CreatedBy = createdBy;
        }

        public User()
        {
            AnalogInputs = new List<AnalogInput>();
            DigitalInputs = new List<DigitalInput>();
        }
    }
}
