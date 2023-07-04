using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class AnalogInputRepository:CrudRepository<AnalogInput>, IAnalogInputRepository
    {
        public AnalogInputRepository(DatabaseContext context) : base(context) { }

    }
}
