using Domain.Entities;

namespace Domain.InterfacesRepositories;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> ObterTarefas(int usuarioId, int pageNumber, int pageSize);
    Task<Tarefa> ObterTarefaPorId(int tarefaId);
    Task AdicionarTarefa(Tarefa tarefa);
    Task AtualizarTarefa(Tarefa tarefa);
    Task RemoverTarefa(Tarefa tarefa);
    Task<bool> SalvarAlteracoesAsync();
}
