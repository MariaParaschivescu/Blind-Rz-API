using Application.Interfaces;
using System.Threading.Tasks;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDeviceRepository DeviceRepository { get; set; }
        public IUserRepository UserRepository { get; set;}
        public IProgramRepository ProgramRepository { get; set; }
        public IRoomRepository RoomRepository { get; set; }
        public ILocationRepository LocationRepository { get; set; }
        public ISettingRepository SettingRepository { get; set; }

        Task SaveChangesAsync();
        void SaveChanges();
    }
}
