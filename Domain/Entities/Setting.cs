using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Setting: Entity<Guid>
    {
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        //public double OpeningPercent { get; set; }
        //public virtual ICollection<Program> Programs { get; set; }
        //public virtual ICollection<Device> Devices { get; set; }
    }
}
