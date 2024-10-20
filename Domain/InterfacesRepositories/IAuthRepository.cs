using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface IAuthRepository
{
    Task<User> Login(string nomeUsuario);
}