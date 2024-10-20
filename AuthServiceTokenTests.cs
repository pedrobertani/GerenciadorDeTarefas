using Xunit;
using Moq;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

public class AuthServiceTokenTests
{
    private readonly AuthService _authService;

    public AuthServiceTokenTests()
    {
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(x => x["Jwt:Key"]).Returns("supersecretkey12345");

        _authService = new AuthService(null, configMock.Object, null); // Pass null for dependencies not being tested
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnValidToken()
    {
        // Arrange
        var usuarioMock = new Usuario
        {
            UsuarioId = 1,
            NomeUsuario = "testuser"
        };

        // Act
        var token = _authService.GenerateJwtToken(usuarioMock);

        // Assert
        Assert.NotNull(token);
        Assert.Contains("eyJ", token); // Verifica se o token é uma string JWT
    }
}
