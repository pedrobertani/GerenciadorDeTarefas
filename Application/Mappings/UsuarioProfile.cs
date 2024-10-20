using Application.DTOs;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {

        CreateMap<UsuarioDto, Usuario>()
            .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()) 
            .ForMember(dest => dest.SenhaSalt, opt => opt.Ignore())  
            .ForMember(dest => dest.DataRegistro, opt => opt.Ignore());

        CreateMap<LoginModel, Usuario>()
           .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()) 
           .ForMember(dest => dest.SenhaSalt, opt => opt.Ignore());
    }
}
