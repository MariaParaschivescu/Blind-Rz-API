using Application.Interfaces;
using Application.UnitOfWork;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlindRZContext _blindRZContext;
        public IDeviceRepository DeviceRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IProgramRepository ProgramRepository { get; set; }
        public IRoomRepository RoomRepository { get; set; }
        public ILocationRepository LocationRepository { get; set; }
        public ISettingRepository SettingRepository { get; set; }

        public UnitOfWork(BlindRZContext rZContext)
        {
            DeviceRepository = new DeviceRepository(rZContext);
            UserRepository = new UserRepository(rZContext);
            ProgramRepository = new ProgramRepository(rZContext);
            RoomRepository = new RoomRepository(rZContext);
            LocationRepository = new LocationRepository(rZContext);
            SettingRepository = new SettingRepository(rZContext);
            _blindRZContext = rZContext;
        }

        public void SaveChanges()
        {
            _blindRZContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _blindRZContext.SaveChangesAsync();
        }
    }
}
