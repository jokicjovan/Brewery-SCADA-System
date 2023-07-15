using Brewery_SCADA_System.DTO;
using System.Text.Json.Serialization;

namespace Brewery_SCADA_System.Models
{
    public class AnalogInput : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
        [JsonIgnore]
        public List<Alarm> Alarms { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }


        public AnalogInput(string description, string driver, string iOAddress, int scanTime, List<Alarm> alarms, bool scanOn, double lowLimit, double highLimit, string unit)
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
            Users = new List<User>();
        }
        public AnalogInput(AnalogInputDTO analogInputDTO)
        {
            Description= analogInputDTO.Description;
            Driver = analogInputDTO.Driver;
            ScanTime = analogInputDTO.ScanTime;
            Alarms = new List<Alarm>();
            ScanOn = analogInputDTO.ScanOn;
            LowLimit = analogInputDTO.LowLimit;
            HighLimit = analogInputDTO.HighLimit;
            Unit = analogInputDTO.Unit;
            Users = new List<User>();
        }

        public AnalogInput()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is AnalogInput input &&
                   Id.Equals(input.Id) &&
                   Description == input.Description &&
                   Driver == input.Driver &&
                   IOAddress == input.IOAddress &&
                   ScanTime == input.ScanTime &&
                   EqualityComparer<List<Alarm>>.Default.Equals(Alarms, input.Alarms) &&
                   ScanOn == input.ScanOn &&
                   LowLimit == input.LowLimit &&
                   HighLimit == input.HighLimit &&
                   Unit == input.Unit &&
                   EqualityComparer<List<User>>.Default.Equals(Users, input.Users);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Description);
            hash.Add(Driver);
            hash.Add(IOAddress);
            hash.Add(ScanTime);
            hash.Add(Alarms);
            hash.Add(ScanOn);
            hash.Add(LowLimit);
            hash.Add(HighLimit);
            hash.Add(Unit);
            hash.Add(Users);
            return hash.ToHashCode();
        }
    }
}
