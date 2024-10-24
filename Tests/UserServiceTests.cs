﻿using Application.DTOs;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using AutoMapper;
using Xunit;
using Application.Services;

namespace Tests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public UserServiceTests()
    {
        // Criando mocks para as dependências
        _userRepositoryMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();

        // Instanciando o UserService com os mocks
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Register_ShouldCallUserRepositoryRegister()
    {
        // Arrange
        var registerDto = new UserDto
        {
            UserName = "testuser",
            Password = "testpassword"
        };

        var usuario = new User
        {
            UserName = "testuser"
        };

        _mapperMock.Setup(m => m.Map<User>(It.IsAny<UserDto>())).Returns(usuario);
        _userRepositoryMock.Setup(repo => repo.Register(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(usuario);

        // Act
        var result = await _userService.Register(registerDto);

        // Assert
        Assert.NotNull(result);
        _mapperMock.Verify(m => m.Map<User>(registerDto), Times.Once);
        _userRepositoryMock.Verify(repo => repo.Register(usuario, registerDto.Password), Times.Once);
    }
}
