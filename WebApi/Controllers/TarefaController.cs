using Application.DTOs;
using Application.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(int pageNumber = 1, int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var tasks = await _tarefaService.ObterTarefas(userId, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _tarefaService.ObterTarefaPorId(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TarefaDto tarefaDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        tarefaDto.UsuarioId = userId;

        var success = await _tarefaService.AdicionarTarefa(tarefaDto);
        if (success) return Ok(tarefaDto);

        return BadRequest("Unable to add task");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TarefaDto tarefaDto)
    {
        var success = await _tarefaService.AtualizarTarefa(id, tarefaDto);
        if (success) return Ok(tarefaDto);

        return BadRequest("Unable to update task");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var success = await _tarefaService.RemoverTarefa(id);
        if (!success) return NotFound();

        return Ok("Task successfully deleted");
    }
}
