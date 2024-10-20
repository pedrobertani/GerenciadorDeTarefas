using Application.InterfacesService;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Application.DTOs;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<UserTaskDto>> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        var tarefas = await _taskRepository.GetAllTasks(userId, pageNumber, pageSize);
        return _mapper.Map<IEnumerable<UserTaskDto>>(tarefas);
    }

    public async Task<UserTaskDto> GetTaskById(int taskId)
    {
        var task = await _taskRepository.GetTaskById(taskId);
        return _mapper.Map<UserTaskDto>(task);
    }

    public async Task<bool> AddTask(UserTaskDto tarefaDto)
    {
        var task = _mapper.Map<UserTask>(tarefaDto);
        await _taskRepository.AddTask(task);
        return await _taskRepository.SaveChangedAsync();
    }

    public async Task<bool> UpdateTask(int taskId, UserTaskDto tarefaDto)
    {
        var task = await _taskRepository.GetTaskById(taskId);
        if (task == null) return false;

        _mapper.Map(tarefaDto, task);
        return await _taskRepository.SaveChangedAsync();
    }

    public async Task<bool> RemoveTask(int taskId)
    {
        var task = await _taskRepository.GetTaskById(taskId);
        if (task == null) return false;

        await _taskRepository.RemoveTask(task);
        return await _taskRepository.SaveChangedAsync();
    }
}
