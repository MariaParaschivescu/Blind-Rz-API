using Domain.Common;

namespace Domain.Entities
{
    public class Device: Entity<Guid>  
    {
        public bool Status { get; set; }
        public string LocalIP { get; set; }
        public string ExternalIP { get; set; }
        public double OpeningPercent { get; set; }
        //public virtual ICollection<Setting> Settings { get; set;}
    }
}
