using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }
    }
}
