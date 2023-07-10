using Brewery_SCADA_System.DTO;
using System.Text.Json.Serialization;

namespace Brewery_SCADA_System.Models
{
    public class DigitalInput : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }

        public DigitalInput(string description, string driver, string iOAddress, int scanTime, bool scanOn)
        {
            Description = description;
            Driver = driver;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            ScanOn = scanOn;
            Users = new List<User>();
        }

        public DigitalInput(DigitalInputDTO digitalInputDTO)
        {
            Description= digitalInputDTO.Description;
            Driver = digitalInputDTO.Driver;    
            ScanTime = digitalInputDTO.ScanTime;
            ScanOn = digitalInputDTO.ScanOn;
            Users = new List<User>();
        }

        public DigitalInput()
        {
            
        }

        public override bool Equals(object? obj)
        {
            return obj is DigitalInput input &&
                   Id.Equals(input.Id) &&
                   Description == input.Description &&
                   Driver == input.Driver &&
                   IOAddress == input.IOAddress &&
                   ScanTime == input.ScanTime &&
                   ScanOn == input.ScanOn &&
                   EqualityComparer<List<User>>.Default.Equals(Users, input.Users);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, Driver, IOAddress, ScanTime, ScanOn, Users);
        }
    }
}
