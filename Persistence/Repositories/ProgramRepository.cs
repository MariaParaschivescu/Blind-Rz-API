using Application.Interfaces;
using Domain.Entities;
using Persistence.Context;
namespace Persistence.Repositories
{
    public class ProgramRepository: Repository<Program>, IProgramRepository
    {
        public ProgramRepository(BlindRZContext dbContext): base(dbContext)
        {

        }
    }
}
