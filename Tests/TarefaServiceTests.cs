using Application.DTOs;
using Application.InterfacesService;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using Xunit;


namespace Tests;

public class taskerviceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ITaskService _taskService;

    public taskerviceTests()
    {
        // Mocks para o repositório e o AutoMapper
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _mapperMock = new Mock<IMapper>();

        // Injetando os mocks no serviço
        _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetTask_ShouldReturntask_WhentaskExist()
    {
        // Arrange
        var taskMock = new List<UserTask>
        {
            new UserTask { TaskId = 1, UserId = 1, Title = "UserTask 1", Description = "Descrição 1" },
            new UserTask { TaskId = 2, UserId = 1, Title = "UserTask 2", Description = "Descrição 2" }
        };

        var taskDtoMock = new List<UserTaskDto>
        {
            new UserTaskDto { TaskId = 1, Title = "UserTask 1", Description = "Descrição 1" },
            new UserTaskDto { TaskId = 2, Title = "UserTask 2", Description = "Descrição 2" }
        };

        _taskRepositoryMock.Setup(repo => repo.GetAllTasks(1, 1, 2))
            .ReturnsAsync(taskMock);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserTaskDto>>(taskMock))
            .Returns(taskDtoMock);

        // Act
        var task = await _taskService.GetAllTasks(1, 1, 2);

        // Assert
        Assert.NotNull(task);
        Assert.Equal(2, task.Count());
    }

    [Fact]
    public async Task ObtertaskPorId_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        var taskMock = new UserTask { TaskId = 1, UserId = 1, Title = "UserTask 1", Description = "Descrição 1" };
        var taskDtoMock = new UserTaskDto { TaskId = 1, Title = "UserTask 1", Description = "Descrição 1" };

        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1))
            .ReturnsAsync(taskMock);

        _mapperMock.Setup(mapper => mapper.Map<UserTaskDto>(taskMock))
            .Returns(taskDtoMock);

        // Act
        var task = await _taskService.GetTaskById(1);

        // Assert
        Assert.NotNull(task);
        Assert.Equal(1, task.TaskId);
    }

    [Fact]
    public async Task Adicionartask_ShouldReturnTrue_WhenTaskIsAddedSuccessfully()
    {
        // Arrange
        var taskDto = new UserTaskDto { TaskId = 1, Title = "Nova UserTask", Description = "Descrição da nova task" };
        var task = new UserTask { TaskId = 1, Title = "Nova UserTask", Description = "Descrição da nova task" };

        _mapperMock.Setup(mapper => mapper.Map<UserTask>(taskDto))
            .Returns(task);

        _taskRepositoryMock.Setup(repo => repo.AddTask(task)).Returns(Task.CompletedTask);
        _taskRepositoryMock.Setup(repo => repo.SaveChangedAsync()).ReturnsAsync(true);

        // Act
        var result = await _taskService.AddTask(taskDto);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Atualizartask_ShouldReturnTrue_WhenTaskIsUpdatedSuccessfully()
    {
        // Arrange
        var taskMock = new UserTask { TaskId = 1, Title = "UserTask Existente", Description = "Descrição existente" };
        var taskDtoMock = new UserTaskDto { TaskId = 1, Title = "UserTask Atualizada", Description = "Descrição atualizada" };

        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1))
            .ReturnsAsync(taskMock);

        _mapperMock.Setup(mapper => mapper.Map(taskDtoMock, taskMock));

        _taskRepositoryMock.Setup(repo => repo.SaveChangedAsync()).ReturnsAsync(true);

        // Act
        var result = await _taskService.UpdateTask(1, taskDtoMock);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Removertask_ShouldReturnTrue_WhenTaskIsDeletedSuccessfully()
    {
        // Arrange
        var taskMock = new UserTask { TaskId = 1, Title = "UserTask a ser removida", Description = "Descrição" };

        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1))
            .ReturnsAsync(taskMock);

        _taskRepositoryMock.Setup(repo => repo.RemoveTask(taskMock)).Returns(Task.CompletedTask);
        _taskRepositoryMock.Setup(repo => repo.SaveChangedAsync()).ReturnsAsync(true);

        // Act
        var result = await _taskService.RemoveTask(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Removertask_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        _taskRepositoryMock.Setup(repo => repo.GetTaskById(1))
            .ReturnsAsync((UserTask)null);

        // Act
        var result = await _taskService.RemoveTask(1);

        // Assert
        Assert.False(result);
    }
}
