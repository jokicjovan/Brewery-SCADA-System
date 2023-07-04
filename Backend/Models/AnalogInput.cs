using Brewery_SCADA_System.DTO;

namespace Brewery_SCADA_System.Models
{
    public class AnalogInput : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public List<Alarm> Alarms { get; set; }
        public bool ScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

        public AnalogInput(string description, string driver, string iOAddress, int scanTime, List<Alarm> alarms, bool scanOn, double lowLimit, double highLimit, string unit)
        {
            Description = description;
            Driver = driver;
            IOAddress = iOAddress;
            ScanTime = scanTime;
            Alarms = alarms;
            ScanOn = scanOn;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Unit = unit;
        }
        public AnalogInput(AnalogInputDTO analogInputDTO)
        {
            Description= analogInputDTO.Description;
            Driver = analogInputDTO.Driver;
            IOAddress = analogInputDTO.IOAddress;
            ScanTime = analogInputDTO.ScanTime;
            Alarms = new List<Alarm>();
            ScanOn = analogInputDTO.ScanOn;
            LowLimit = analogInputDTO.LowLimit;
            HighLimit = analogInputDTO.HighLimit;
            Unit = analogInputDTO.Unit;
        }

        public AnalogInput()
        {
        }
    }
}
