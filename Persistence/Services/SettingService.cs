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
    public class SettingService : ISettingService
    {

        private readonly ISettingRepository _settingRepository;
        private readonly IStringLocalizer<SettingService> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SettingService> localizer)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = unitOfWork.SettingRepository;
            _localizer = localizer;
            _mapper = mapper;
        }
        public Task<Result<Guid>> AddSettingAsync(Setting setting)
        {
            var newSetting = _settingRepository.Create(setting);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(newSetting.Id);
        }

        public Task<Result<Guid>> DeleteSettingAsync(Guid settingId)
        {
            var deletedSetting = _settingRepository.Delete(settingId);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(settingId);

        }

        public Task<Result<Guid>> UpdateSettingAsync(Guid id)
        {
            var setting = _settingRepository.GetById(id).Result;
            if (setting == null) throw new EntityNotFoundException(String.Format(_localizer["setting.notfound"], id));
            var updatedSetting = _settingRepository.Update(setting);
            _unitOfWork.SaveChangesAsync();
            return Result<Guid>.SuccessAsync(id);
        }
    }
}
