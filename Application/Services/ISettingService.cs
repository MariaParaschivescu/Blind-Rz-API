using Application.Wrapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ISettingService
    {
        Task<Result<Guid>> AddSettingAsync(Setting setting);
        Task<Result<Guid>> UpdateSettingAsync(Guid id);
        Task<Result<Guid>> DeleteSettingAsync(Guid settingId);
    }
}
