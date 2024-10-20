using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface IUserRepository
{
    Task<Usuario> Register(Usuario usuario, string senha);
}
