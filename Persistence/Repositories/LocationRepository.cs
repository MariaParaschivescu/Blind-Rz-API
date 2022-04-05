using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class LocationRepository: Repository<Location>, ILocationRepository
    {
        public LocationRepository(BlindRZContext dbContext): base(dbContext)
        {

        }
    }
}
