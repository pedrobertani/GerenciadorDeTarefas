using Application.DTOs;
using Domain.Entities;

namespace Application.InterfacesService;

public interface IUserService
{
    Task<User> Register(UserDto registerDto);
}