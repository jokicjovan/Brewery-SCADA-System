using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class AnalogInputDTO
    {
        public string Description { get; set; }
        public string Driver { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

        public AnalogInputDTO(string description, string driver, int scanTime, bool scanOn, double lowLimit, double highLimit, string unit)
        {
            Description = description;
            Driver = driver;
            ScanTime = scanTime;
            ScanOn = scanOn;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Unit = unit;
        }
    }
}
