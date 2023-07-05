using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class AlarmReportsDTO
    {
        public Guid AlarmId { get; set; }
        public AlarmType Type { get; set; }
        public AlarmPriority Priority { get; set; }
        public Double EdgeValue { get; set; }
        public String Unit { get; set; }
        public DateTime Timestamp { get; set; }

        public AlarmReportsDTO(Guid alarmId, AlarmType type, AlarmPriority priority, double edgeValue, string unit, DateTime timestamp)
        {
            AlarmId = alarmId;
            Type = type;
            Priority = priority;
            EdgeValue = edgeValue;
            Unit = unit;
            Timestamp = timestamp;
        }

        public AlarmReportsDTO()
        {
            
        }
    }
}
