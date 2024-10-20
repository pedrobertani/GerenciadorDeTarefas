using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests
{
    public class AuthServiceTokenTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IMapper> _mapperMock;

        public AuthServiceTokenTests()
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
        public void GenerateJwtToken_ShouldReturnValidToken()
        {
            // Arrange
            var usuario = new User
            {
                UserId = 1,
                UserName = "test_user",
                HashPassword = new byte[64],
                SaltPassword = new byte[128] 
            };

            // Act
            var token = _authService.GenerateJwtToken(usuario);

            // Assert
            Assert.NotNull(token); 
        }
    }
}
