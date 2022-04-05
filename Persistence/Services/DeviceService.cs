using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services;
using Application.UnitOfWork;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IStringLocalizer<DeviceService> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<DeviceService> localizer)
        {
            _unitOfWork = unitOfWork;
            _deviceRepository = unitOfWork.DeviceRepository;
            _mapper = mapper;
            _localizer = localizer;
        }
        public Task<Result<Guid>> AddDeviceAsync(Device device)
        {
            var newDevice = _deviceRepository.Create(device);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(newDevice.Id);
        }

        public Task<Result<Guid>> DeleteDeviceAsync(Guid deviceId)
        {
            var toDeleteDevice = _deviceRepository.Delete(deviceId);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(deviceId);
        }

        public Task<Result<Guid>> GetDeviceDetailAsync(Guid deviceId)
        {
            var device = _deviceRepository.GetById(deviceId).Result;
            if (device == null) throw new EntityNotFoundException(String.Format(_localizer["device.notfound"], deviceId));
            return Result<Guid>.SuccessAsync(deviceId);
        }

        public Task<Result<Guid>> UpdateDeviceAsync(Guid id)
        {
            var device = _deviceRepository.GetById(id).Result;
            if (device == null) throw new EntityNotFoundException(String.Format(_localizer["device.notfound"], id));
            var updatedDevice = _deviceRepository.Update(device);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(id);

        }

        public void TurnOnDevice(Guid id)
        {
            var device = _deviceRepository.GetById(id).Result;
            if (device == null) throw new EntityNotFoundException(String.Format(_localizer["device.notfound"], id));
        }

        public void TurnOffDevice(Guid id)
        {
            var device = _deviceRepository.GetById(id).Result;
        }
    }
}
