using AutoMapper;
using Application.DTOs;
using Domain.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
            .ForMember(dest => dest.SaltPassword, opt => opt.Ignore());
    }
}
