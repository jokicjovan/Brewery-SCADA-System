using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class DigitalInputRepository : CrudRepository<DigitalInput>, IDigitalInputRepository
    {
        public DigitalInputRepository(DatabaseContext context) : base(context) { }
    }
}
