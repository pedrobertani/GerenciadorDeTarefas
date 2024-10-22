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
    public async Task<(IEnumerable<UserTaskDto> Tasks, int TotalCount)> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        var (tasks, totalCount) = await _taskRepository.GetAllTasks(userId, pageNumber, pageSize);
        var tasksDto = _mapper.Map<IEnumerable<UserTaskDto>>(tasks);
        return (tasksDto, totalCount); 
    }


    public async Task<UserTaskDto> GetTaskById(int taskId)
    {
        var task = await _taskRepository.GetTaskById(taskId);
        return _mapper.Map<UserTaskDto>(task);
    }

    public async Task<bool> AddTask(UserTaskDto tarefaDto)
    {
        var task = _mapper.Map<UserTask>(tarefaDto);
        return await _taskRepository.AddTask(task);
    }

    public async Task<bool> UpdateTask(UserTaskDto tarefaDto)
    {
        var task = await _taskRepository.GetTaskById(tarefaDto.TaskId);
        if (task == null) return false;

        _mapper.Map(tarefaDto, task);
        return await _taskRepository.UpdateTask(task);
    }

    public async Task<bool> CompleteTask(int taskId)
    {
        return await _taskRepository.MakeCompleted(taskId);
    }


    public async Task<bool> RemoveTask(int taskId)
    {
        var task = await _taskRepository.GetTaskById(taskId);
        if (task == null) return false;

        return await _taskRepository.RemoveTask(task);
    }
}
