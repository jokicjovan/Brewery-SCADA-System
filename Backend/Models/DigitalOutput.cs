namespace Brewery_SCADA_System.Models
{
    public class DigitalOutput : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double InitialValue { get; set; }

        public DigitalOutput(string description, string iOAddress, double initialValue)
        {
            Description = description;
            IOAddress = iOAddress;
            InitialValue = initialValue;
        }

        public DigitalOutput()
        {
            
        }
    }
}
