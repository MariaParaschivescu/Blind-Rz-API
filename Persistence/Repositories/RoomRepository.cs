using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class RoomRepository: Repository<Room>, IRoomRepository
    {
        public RoomRepository(BlindRZContext dbContext): base(dbContext)
        {

        }
    }
}
