using Application.DTOs;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.HashPassword, opt => opt.Ignore()) 
            .ForMember(dest => dest.SaltPassword, opt => opt.Ignore())  
            .ForMember(dest => dest.DateRegister, opt => opt.Ignore());

        CreateMap<LoginModel, User>()
           .ForMember(dest => dest.HashPassword, opt => opt.Ignore()) 
           .ForMember(dest => dest.SaltPassword, opt => opt.Ignore());
    }
}
