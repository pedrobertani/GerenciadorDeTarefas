using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface IUserRepository
{
    Task<User> Register(User usuario, string senha);
}
