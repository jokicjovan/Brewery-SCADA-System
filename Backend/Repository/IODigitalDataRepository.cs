using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class IODigitalDataRepository : CrudRepository<IODigitalData>, IIODigitalDataRepository
    {
        public IODigitalDataRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
