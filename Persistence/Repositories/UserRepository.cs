using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(BlindRZContext rZContext): base(rZContext)
        {
        }
    }
}
