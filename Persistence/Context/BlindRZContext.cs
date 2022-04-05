using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class BlindRZContext: DbContext
    {
        public BlindRZContext(DbContextOptions<BlindRZContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
