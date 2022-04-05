using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Device
{
    public class DeviceDTO
    {
        public bool Status { get; set; } = false;
        public string LocalIP { get; set; }
        public string ExternalIP { get; set; }
        [Required]
        public double OpeningPercent { get; set; } = 0;
    }
}
