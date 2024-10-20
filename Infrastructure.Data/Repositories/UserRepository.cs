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

        public async Task<Usuario> Register(Usuario usuario, string senha)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                usuario.SenhaSalt = hmac.Key;
                usuario.SenhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
