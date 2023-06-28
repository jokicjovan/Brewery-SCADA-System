namespace Brewery_SCADA_System.Models
{
    public class AnalogOutput : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

        public AnalogOutput(string description, string iOAddress, double initialValue, double lowLimit, double highLimit, string unit)
        {
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Unit = unit;
        }

        public AnalogOutput()
        {
            
        }
    }
}
