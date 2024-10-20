using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface IAuthRepository
{
    Task<Usuario> Login(string nomeUsuario);
}