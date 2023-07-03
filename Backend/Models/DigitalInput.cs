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

        public DigitalInput(string description, string driver, string iOAddress, int scanTime, bool scanOn)
        {
            Description = description;
            Driver = driver;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            ScanOn = scanOn;
        }

        public DigitalInput()
        {
            
        }
    }
}
