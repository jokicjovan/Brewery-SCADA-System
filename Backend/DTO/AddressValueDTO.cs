namespace Brewery_SCADA_System.DTO
{
    public class AddressValueDTO
    {
        public Guid Id { get; set; }
        public Double Value { get; set; }

        public AddressValueDTO(Guid id, double value)
        {
            Id = id;
            Value = value;
        }

        public AddressValueDTO()
        {
        }
    }
}
