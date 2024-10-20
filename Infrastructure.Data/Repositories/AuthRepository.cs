using Domain.Entities;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;

    public AuthRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> Login(string nomeUsuario)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.UserName == nomeUsuario);
    }
}
