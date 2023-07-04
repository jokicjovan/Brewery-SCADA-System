using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO;

public class AlarmDTO
{
    public AlarmType Type { get; set; }
    public AlarmPriority Priority { get; set; }
    public double EdgeValue { get; set; }
    public string Unit { get; set; }
    public Guid TagId { get; set; }
}