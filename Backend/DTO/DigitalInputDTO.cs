namespace Brewery_SCADA_System.DTO
{
    public class DigitalInputDTO
    {
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }

        public DigitalInputDTO(string description, string driver, string iOAddress, int scanTime, bool scanOn)
        {
            Description = description;
            Driver = driver;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            ScanOn = scanOn;
        }
    }
}
