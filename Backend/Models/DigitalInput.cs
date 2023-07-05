using Brewery_SCADA_System.DTO;

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
            IOAddress = digitalInputDTO.IOAddress;
            ScanTime = digitalInputDTO.ScanTime;
            ScanOn = digitalInputDTO.ScanOn;
            Users = new List<User>();
        }

        public DigitalInput()
        {
            
        }
    }
}
