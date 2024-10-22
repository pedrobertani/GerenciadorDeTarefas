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

    public async Task<(IEnumerable<UserTask> Tasks, int TotalCount)> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        var totalTasks = await _context.Task.CountAsync(t => t.UserId == userId); 
        var tasks = await _context.Task
            .Where(t => t.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tasks, totalTasks); // Retorna as tarefas e o total como uma tupla
    }


    public async Task<UserTask> GetTaskById(int taskId)
    {
        return await _context.Task.FindAsync(taskId);
    }

    public async Task<bool> AddTask(UserTask task)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.Task.AddAsync(task);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();
                return true;
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw; 
        }

        return false;
    }

    public async Task<bool> UpdateTask(UserTask task)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var existingTask = await _context.Task.FindAsync(task.TaskId);
            if (existingTask == null)
            {
                return false; 
            }

            existingTask.Title = task.Title; 
            existingTask.Description = task.Description; 
                                                         

            _context.Task.Update(existingTask);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();
                return true;
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return false;
    }

    public async Task<bool> MakeCompleted(int taskId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Encontre a tarefa existente pelo ID
            var existingTask = await _context.Task.FindAsync(taskId);
            if (existingTask == null)
            {
                return false; // Tarefa não encontrada
            }

            // Marque a tarefa como concluída
            existingTask.Completed = true;

            // Atualize a tarefa no contexto
            _context.Task.Update(existingTask);

            // Salve as alterações
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();
                return true; 
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return false; 
    }



    public async Task<bool> RemoveTask(UserTask task)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Task.Remove(task);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                await transaction.CommitAsync();
                return true;
            }
        }
        catch
        {
            await transaction.RollbackAsync();
            throw; 
        }

        return false;
    }
}
