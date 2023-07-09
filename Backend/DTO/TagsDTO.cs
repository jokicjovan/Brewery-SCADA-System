using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class TagsDTO
    {

        public List<AnalogInputValueDTO> AnalogInputs { get; set; }
        public List<DigitalInputValueDTO> DigitalInputs { get; set; }

        public TagsDTO(List<AnalogInputValueDTO> analogInputs, List<DigitalInputValueDTO> digitalInputs)
        {
            AnalogInputs = analogInputs;
            DigitalInputs = digitalInputs;
        }


    }
    public class AnalogInputValueDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public double Value { get; set; }

        public string Unit { get; set; }

        public AnalogInputValueDTO()
        {
        }

        public AnalogInputValueDTO(AnalogInput input,double value)
        {
            Id=input.Id;
            Description=input.Description;
            Driver=input.Driver;
            IOAddress=input.IOAddress;
            ScanTime=input.ScanTime;
            ScanOn=input.ScanOn;
            LowLimit = input.LowLimit;
            HighLimit = input.HighLimit;
            Value = value;
            Unit = input.Unit;
        }
    }
    public class DigitalInputValueDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public string IOAddress { get; set; }
        public int ScanTime { get; set; }
        public bool ScanOn { get; set; }
        public double Value { get; set; }
        public DigitalInputValueDTO()
        {
        }

        public DigitalInputValueDTO(DigitalInput input, double value)
        {
            Id=input.Id;
            Description=input.Description;
            Driver=input.Driver;
            IOAddress=input.IOAddress;
            ScanTime=input.ScanTime;
            ScanOn=input.ScanOn;
            Value = value;
        }
        }
}
