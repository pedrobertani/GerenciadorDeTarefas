using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface ITaskRepository
{
    Task<(IEnumerable<UserTask> Tasks, int TotalCount)> GetAllTasks(int userId, int pageNumber, int pageSize);
    Task<UserTask> GetTaskById(int taskId);
    Task<bool> AddTask(UserTask task);
    Task<bool> UpdateTask(UserTask task);
    Task<bool> RemoveTask(UserTask task);
    Task<bool> MakeCompleted(int taskId);
}
