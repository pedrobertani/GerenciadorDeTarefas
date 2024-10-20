using Domain.Entities;

namespace Application.InterfacesService;

public interface IAuthService
{
    Task<Usuario> Login(string nomeUsuario, string senha);
    string GenerateJwtToken(Usuario usuario);
}