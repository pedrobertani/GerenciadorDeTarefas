using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface ITaskRepository
{
    Task<IEnumerable<UserTask>> GetAllTasks(int userId, int pageNumber, int pageSize);
    Task<UserTask> GetTaskById(int taskId);
    Task AddTask(UserTask task);
    Task UpdateTask(UserTask task);
    Task RemoveTask(UserTask task);
    Task<bool> SaveChangedAsync();
}
