using Domain.Entities;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Hash e salt da senha
                    (usuario.HashPassword, usuario.SaltPassword) = HashPassword(senha);

                    // Adicionar o novo usuário ao banco de dados
                    await _context.User.AddAsync(usuario);
                    await _context.SaveChangesAsync();

                    // Confirma a transação
                    await transaction.CommitAsync();

                    return usuario;
                }
                catch (Exception ex)
                {
                    // Em caso de erro, reverte a transação
                    await transaction.RollbackAsync();
                    throw; // Relança a exceção para tratamento posterior, se necessário
                }
            }
        }


        public async Task<User> GetByUsername(string username)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        private (byte[] hashPassword, byte[] saltPassword) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
        }
    }
}
