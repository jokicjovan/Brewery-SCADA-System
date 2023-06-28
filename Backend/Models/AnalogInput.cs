namespace Brewery_SCADA_System.Models
{
    public class AnalogInput : BaseEntity
    {
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public List<Alarm> Alarms { get; set; }
        public bool ScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }

        public AnalogInput(string description, string driver, string iOAddress, int scanTime, List<Alarm> alarms, bool scanOn, double lowLimit, double highLimit, string unit, double value)
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
            Value = value;
        }

        public AnalogInput()
        {
        }
    }
}
