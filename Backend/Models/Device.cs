namespace Brewery_SCADA_System.Models
{
    public class Device: IBaseEntity
    {
        public Guid Id { get; set; }
        public String Address {  get; set; }

        public Double Value { get; set; }

        public Device(string address, double value)
        {
            Id = Guid.NewGuid();
            Address = address;
            Value = value;
        }

    }
}
