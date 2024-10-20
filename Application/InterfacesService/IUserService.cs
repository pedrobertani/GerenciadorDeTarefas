using Application.DTOs;
using Domain.Entities;

namespace Application.InterfacesService;

public interface IUserService
{
    Task<Usuario> Register(UsuarioDto registerDto);
}