namespace Brewery_SCADA_System.Models
{
    public class Alarm : IBaseEntity
    {
        public Guid Id { get; set; }
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public Double EdgeValue { get; set; }
        public String Unit { get; set; }

        public Alarm()
        {
            
        }

        public Alarm(Guid id, AlarmType type, AlarmPriority priority, double edgeValue, string unit)
        {
            Id = id;
            Type = type;
            Priority = priority;
            EdgeValue = edgeValue;
            Unit = unit;
        }
    }
}
