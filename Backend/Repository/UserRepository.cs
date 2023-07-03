﻿using Brewery_SCADA_System.Database;
using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Repository
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) { }

        public async Task<User> FindByEmail(String email) 
        {
            return await _entities.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}