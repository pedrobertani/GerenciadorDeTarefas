using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using Xunit;

namespace Tests;


public class TaskServiceTests
{
    private readonly TaskService _taskService;
    private readonly Mock<ITaskRepository> _taskRepositoryMock = new Mock<ITaskRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    public TaskServiceTests()
    {
        _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllTasks_ShouldReturnTasksAndTotalCount()
    {
        // Arrange
        var userId = 1;
        var pageNumber = 1;
        var pageSize = 10;
        var tasks = new List<UserTask> { new UserTask { TaskId = 1, Title = "Test Task" } };
        var tasksDto = new List<UserTaskDto> { new UserTaskDto { TaskId = 1, Title = "Test Task" } };

        _taskRepositoryMock.Setup(r => r.GetAllTasks(userId, pageNumber, pageSize))
            .ReturnsAsync((tasks, tasks.Count));

        _mapperMock.Setup(m => m.Map<IEnumerable<UserTaskDto>>(tasks)).Returns(tasksDto);

        // Act
        var result = await _taskService.GetAllTasks(userId, pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tasksDto, result.Tasks);
        Assert.Equal(tasks.Count, result.TotalCount);
    }

    [Fact]
    public async Task GetTaskById_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        var taskId = 1;
        var task = new UserTask { TaskId = taskId, Title = "Test Task" };
        var taskDto = new UserTaskDto { TaskId = taskId, Title = "Test Task" };

        _taskRepositoryMock.Setup(r => r.GetTaskById(taskId)).ReturnsAsync(task);
        _mapperMock.Setup(m => m.Map<UserTaskDto>(task)).Returns(taskDto);

        // Act
        var result = await _taskService.GetTaskById(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskDto, result);
    }

    [Fact]
    public async Task AddTask_ShouldReturnTrue_WhenTaskIsAdded()
    {
        // Arrange
        var taskDto = new UserTaskDto { TaskId = 1, Title = "New Task" };
        var task = new UserTask { TaskId = 1, Title = "New Task" };

        _mapperMock.Setup(m => m.Map<UserTask>(taskDto)).Returns(task);
        _taskRepositoryMock.Setup(r => r.AddTask(task)).ReturnsAsync(true);

        // Act
        var result = await _taskService.AddTask(taskDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateTask_ShouldReturnTrue_WhenTaskIsUpdated()
    {
        // Arrange
        var taskDto = new UserTaskDto { TaskId = 1, Title = "Updated Task" };
        var task = new UserTask { TaskId = 1, Title = "Test Task" };

        _taskRepositoryMock.Setup(r => r.GetTaskById(taskDto.TaskId)).ReturnsAsync(task);
        _taskRepositoryMock.Setup(r => r.UpdateTask(task)).ReturnsAsync(true);

        // Act
        var result = await _taskService.UpdateTask(taskDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CompleteTask_ShouldReturnTrue_WhenTaskIsCompleted()
    {
        // Arrange
        var taskId = 1;

        _taskRepositoryMock.Setup(r => r.MakeCompleted(taskId)).ReturnsAsync(true);

        // Act
        var result = await _taskService.CompleteTask(taskId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveTask_ShouldReturnTrue_WhenTaskIsRemoved()
    {
        // Arrange
        var taskId = 1;
        var task = new UserTask { TaskId = taskId };

        _taskRepositoryMock.Setup(r => r.GetTaskById(taskId)).ReturnsAsync(task);
        _taskRepositoryMock.Setup(r => r.RemoveTask(task)).ReturnsAsync(true);

        // Act
        var result = await _taskService.RemoveTask(taskId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveTask_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = 1;
        _taskRepositoryMock.Setup(r => r.GetTaskById(taskId)).ReturnsAsync((UserTask)null);

        // Act
        var result = await _taskService.RemoveTask(taskId);

        // Assert
        Assert.False(result);
    }
}
