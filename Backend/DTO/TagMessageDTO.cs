namespace Brewery_SCADA_System.DTO
{
    public class TagMessageDTO
    {
        public string Message { get; set; }

        public TagMessageDTO() { }

        public TagMessageDTO(string message)
        {
            Message = message;
        }
    }
}
