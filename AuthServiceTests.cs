using Application.InterfacesService;
using Application.Services;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class AuthServiceTests
{
    private readonly Mock<IAuthRepository> _authRepositoryMock;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        _authRepositoryMock = new Mock<IAuthRepository>();
        _authService = new AuthService(_authRepositoryMock.Object, null, null); // Passe null para config e mapper se não for usado nos testes
    }

    [Fact]
    public async Task Login_ShouldReturnUser_WhenCredentialsAreCorrect()
    {
        // Arrange
        var usuarioMock = new Usuario
        {
            UsuarioId = 1,
            NomeUsuario = "testuser",
            SenhaHash = new byte[64], // Mock a senha hash correta
            SenhaSalt = new byte[128] // Mock o salt correto
        };

        _authRepositoryMock.Setup(x => x.Login("testuser")).ReturnsAsync(usuarioMock);

        // Act
        var result = await _authService.Login("testuser", "correctPassword");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.NomeUsuario);
    }

    [Fact]
    public async Task Login_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange
        _authRepositoryMock.Setup(x => x.Login("invalidUser")).ReturnsAsync((Usuario)null);

        // Act
        var result = await _authService.Login("invalidUser", "anyPassword");

        // Assert
        Assert.Null(result);
    }
}
