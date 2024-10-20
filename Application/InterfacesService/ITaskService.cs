using Application.DTOs;
using Domain.Entities;

namespace Application.InterfacesService;

public interface ITaskService
{
    Task<IEnumerable<UserTaskDto>> GetAllTasks(int userId, int pageNumber, int pageSize);
    Task<UserTaskDto> GetTaskById(int taskId);
    Task<bool> AddTask(UserTaskDto task);
    Task<bool> UpdateTask(int taskId, UserTaskDto task);
    Task<bool> RemoveTask(int taskId);
}
