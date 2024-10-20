using Application.DTOs;
using Domain.Entities;

namespace Application.InterfacesService;

public interface ITarefaService
{
    Task<IEnumerable<TarefaDto>> ObterTarefas(int usuarioId, int pageNumber, int pageSize);
    Task<TarefaDto> ObterTarefaPorId(int tarefaId);
    Task<bool> AdicionarTarefa(TarefaDto tarefa);
    Task<bool> AtualizarTarefa(int tarefaId, TarefaDto tarefa);
    Task<bool> RemoverTarefa(int tarefaId);
}
