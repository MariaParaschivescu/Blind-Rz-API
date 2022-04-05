using Application.DTOs;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDTO>().ReverseMap();
            CreateMap<RegisterUserDTO, User>();
            CreateMap<ResetPasswordDTO, User>();
        }
    }
}
