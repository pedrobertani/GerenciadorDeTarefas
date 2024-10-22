using Application.DTOs;
using Application.InterfacesService;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(int pageNumber = 1, int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var (tasks, totalCount) = await _taskService.GetAllTasks(userId, pageNumber, pageSize);

        return Ok(new { tasks, totalCount });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] UserTaskDto tarefaDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        tarefaDto.UserId = userId;

        var success = await _taskService.AddTask(tarefaDto);
        if (success) return Ok(tarefaDto);

        return BadRequest("Unable to add task");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] UserTaskDto tarefaDto)
    {
        var success = await _taskService.UpdateTask(tarefaDto);
        if (success) return Ok(tarefaDto);

        return BadRequest("Unable to update task");
    }

    [HttpPut("{taskId}/complete")]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        var result = await _taskService.CompleteTask(taskId);

        if (result)
        {
            return NoContent(); 
        }

        return NotFound(); 
    }


    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var success = await _taskService.RemoveTask(taskId);
        if (!success)
        {
            return NotFound(); 
        }
        return NoContent();
    }

}
