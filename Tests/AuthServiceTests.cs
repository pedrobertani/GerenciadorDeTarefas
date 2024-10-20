using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly Mock<IAuthRepository> _authRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IMapper> _mapperMock;

    public AuthServiceTests()
    {
        // Criando mocks para todas as dependências
        _authRepositoryMock = new Mock<IAuthRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _mapperMock = new Mock<IMapper>();

        // Definindo o valor da chave JWT para os testes com um valor de pelo menos 64 caracteres
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("this_is_a_secure_jwt_key_that_is_long_enough_to_meet_the_requirements");

        // Instanciando o AuthService com os mocks
        _authService = new AuthService(
            _authRepositoryMock.Object,
            _configurationMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Login_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange
        // Simulando que o repositório retorna null quando um usuário não é encontrado
        _authRepositoryMock.Setup(repo => repo.Login(It.IsAny<string>())).ReturnsAsync((Usuario)null);

        // Act
        var result = await _authService.Login("unknown_user", "password");

        // Assert
        Assert.Null(result);
    }
}
