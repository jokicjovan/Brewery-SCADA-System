using Brewery_SCADA_System.DTO;

namespace Brewery_SCADA_System.Hubs
{
    public interface ITagClient
    {
        Task ReceiveMessage(TagMessageDTO message);
    }
}
