namespace Brewery_SCADA_System.Models
{
    public class IODigitalData : IBaseEntity
    {
        public Guid Id { get; set; }
        public String Address { get; set; }
        public Double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public IODigitalData()
        {
            
        }

        public IODigitalData(Guid id, string address, double value, DateTime timestamp)
        {
            Id = id;
            Address = address;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
