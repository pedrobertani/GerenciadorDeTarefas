using Domain.Entities;
using Domain.InterfacesRepositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly DataContext _context;

    public TarefaRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tarefa>> ObterTarefas(int usuarioId, int pageNumber, int pageSize)
    {
        return await _context.Tarefas
            .Where(t => t.UsuarioId == usuarioId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Tarefa> ObterTarefaPorId(int tarefaId)
    {
        return await _context.Tarefas.FindAsync(tarefaId);
    }

    public async Task AdicionarTarefa(Tarefa tarefa)
    {
        await _context.Tarefas.AddAsync(tarefa);
    }

    public async Task AtualizarTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
    }

    public async Task RemoverTarefa(Tarefa tarefa)
    {
        _context.Tarefas.Remove(tarefa);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
