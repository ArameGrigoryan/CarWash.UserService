using AutoMapper;
using CarWash.UserService.Application.DTOs;
using CarWash.UserService.Domain.Entities;

namespace CarWash.UserService.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Hash անում ենք Service-ում
    }
}