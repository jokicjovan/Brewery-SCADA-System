namespace Brewery_SCADA_System.Models
{
    public class AlarmAlert : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid AlarmId { get; set; }
        public DateTime Timestamp { get; set; }
        public Double Value { get; set; }
    }
}
