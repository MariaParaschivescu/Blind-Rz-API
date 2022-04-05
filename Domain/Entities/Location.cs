using Domain.Common;

namespace Domain.Entities
{
    public class Location : Entity<Guid>
    {
        public virtual ICollection<Room> Rooms { get; set; }
        public string Address { get; set; }
    }
}
