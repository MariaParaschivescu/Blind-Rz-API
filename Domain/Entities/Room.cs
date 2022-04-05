using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Room: Entity<Guid>
    {
        [Display(Name ="Room Name")]
        [Required]
        public string RoomName { get; set; }
        public virtual ICollection<Program> Programs { get; set;}
    }
}
