using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class AlarmReportsDTO
    {
        public Alarm Alarm { get; set; }
        public DateTime Timestamp { get; set; }
        public double? Value { get; set; }

        public AlarmReportsDTO(Alarm alarm, DateTime timestamp)
        {
            Alarm = alarm;
            Timestamp = timestamp;
        }

        public AlarmReportsDTO(Alarm alarm, DateTime timestamp, double value)
        {
            Alarm = alarm;
            Timestamp = timestamp;
            Value = value;
        }

        public AlarmReportsDTO()
        {
            
        }
    }
}
