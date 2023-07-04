using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class IOAnalogDataRepository : CrudRepository<IOAnalogData>, IIOAnalogDataRepository
    {
        public IOAnalogDataRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
