using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class DigitalInputRepository : CrudRepository<DigitalInput>, IDigitalInputRepository
    {
        public DigitalInputRepository(DatabaseContext context) : base(context) { }
    }
}
