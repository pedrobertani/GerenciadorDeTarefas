using Domain.Entities;

namespace Application.InterfacesService;

public interface IAuthService
{
    Task<User> Login(string nomeUsuario, string senha);
    string GenerateJwtToken(User usuario);
}