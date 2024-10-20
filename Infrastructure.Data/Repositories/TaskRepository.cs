using Domain.Entities;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTask>> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        return await _context.Task
            .Where(t => t.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<UserTask> GetTaskById(int taskId)
    {
        return await _context.Task.FindAsync(taskId);
    }

    public async Task AddTask(UserTask task)
    {
        await _context.Task.AddAsync(task);
    }

    public async Task UpdateTask(UserTask task)
    {
        _context.Task.Update(task);
    }

    public async Task RemoveTask(UserTask task)
    {
        _context.Task.Remove(task);
    }

    public async Task<bool> SaveChangedAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
