using Application.InterfacesService;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly byte[] _jwtKey;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepository, IConfiguration configuration, IMapper mapper)
    {
        _authRepository = authRepository;

        var jwtKey = configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 64) 
            throw new ArgumentException("A chave JWT deve ter pelo menos 64 caracteres.", nameof(jwtKey));

        _jwtKey = Encoding.UTF8.GetBytes(jwtKey); 
        _mapper = mapper;
    }

    public async Task<User> Login(string nomeUsuario, string senha)
    {
        var usuario = await _authRepository.Login(nomeUsuario);
        if (usuario == null) return null;

        using (var hmac = new System.Security.Cryptography.HMACSHA512(usuario.SaltPassword))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            if (!computedHash.SequenceEqual(usuario.HashPassword)) return null;
        }

        return usuario;
    }

    public string GenerateJwtToken(User usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var now = DateTime.UtcNow;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UserId.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName)
            }),
            Expires = now.AddHours(1),
            NotBefore = now,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtKey), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
