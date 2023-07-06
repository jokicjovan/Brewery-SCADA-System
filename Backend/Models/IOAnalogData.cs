namespace Brewery_SCADA_System.Models
{
    public class IOAnalogData : IBaseEntity
    {
        public Guid Id { get; set; }
        public String IOAddress { get; set; }
        public Double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid TagId { get; set; }
        public IOAnalogData()
        {
            
        }

        public IOAnalogData(Guid id, string iOAddress, double value, DateTime timestamp, Guid tagId)
        {
            Id = id;
            IOAddress = iOAddress;
            Value = value;
            Timestamp = timestamp;
            TagId = tagId;
        }

    }
}
