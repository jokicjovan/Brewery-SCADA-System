using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class TagsDTO
    {

        public List<AnalogInput> AnalogInputs { get; set; }
        public List<DigitalInput> DigitalInputs { get; set; }

        public TagsDTO(List<AnalogInput> analogInputs, List<DigitalInput> digitalInputs)
        {
            AnalogInputs = analogInputs;
            DigitalInputs = digitalInputs;
        }


    }
}
