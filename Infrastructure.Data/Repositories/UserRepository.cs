using Domain.Entities;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;


namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User usuario, string senha)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                usuario.SaltPassword = hmac.Key;
                usuario.HashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }

            await _context.User.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
