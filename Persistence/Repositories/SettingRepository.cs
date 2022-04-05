using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class SettingRepository: Repository<Setting>, ISettingRepository
    {
        public SettingRepository(BlindRZContext dbContext) : base(dbContext)
        {

        }
    }
}
