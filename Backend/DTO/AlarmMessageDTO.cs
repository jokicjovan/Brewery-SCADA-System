namespace Brewery_SCADA_System.DTO
{
    public class AlarmMessageDTO
    {
        public string Message { get; set; }

        public AlarmMessageDTO() { }

        public AlarmMessageDTO(string message)
        {
            Message = message;
        }
    }
}
