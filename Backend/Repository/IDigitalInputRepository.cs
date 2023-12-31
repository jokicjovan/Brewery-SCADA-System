﻿using Brewery_SCADA_System.Models;

namespace Brewery_SCADA_System.Repository
{
    public interface IDigitalInputRepository : ICrudRepository<DigitalInput>
    {
        public Task<DigitalInput> FindByIdWithUsers(Guid id);


    }
}
