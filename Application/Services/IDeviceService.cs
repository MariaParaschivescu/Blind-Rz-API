using Application.DTOs.Device;
using Application.Wrapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDeviceService
    {
        Task<Result<Guid>> AddDeviceAsync(Device device);
        Task<Result<Guid>> DeleteDeviceAsync(Guid deviceId);
        Task<Result<Guid>> UpdateDeviceAsync(Guid id);
        Task<Result<Guid>> GetDeviceDetailAsync(Guid deviceId);
    }
}
