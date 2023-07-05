using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.DTO
{
    public class TagReportsDto
    {
        public List<IOAnalogData> IoAnalogDataList { get; set; }
        public List<IODigitalData> IoDigitalDataList { get; set; }

        public TagReportsDto()
        {
            IoAnalogDataList = new List<IOAnalogData>();
            IoDigitalDataList = new List<IODigitalData>();
        }
    }
}
