using Application.InterfacesService;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Application.DTOs;

namespace Application.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IMapper _mapper;

    public TarefaService(ITarefaRepository tarefaRepository, IMapper mapper)
    {
        _tarefaRepository = tarefaRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<TarefaDto>> ObterTarefas(int usuarioId, int pageNumber, int pageSize)
    {
        var tarefas = await _tarefaRepository.ObterTarefas(usuarioId, pageNumber, pageSize);
        return _mapper.Map<IEnumerable<TarefaDto>>(tarefas);
    }

    public async Task<TarefaDto> ObterTarefaPorId(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterTarefaPorId(tarefaId);
        return _mapper.Map<TarefaDto>(tarefa);
    }

    public async Task<bool> AdicionarTarefa(TarefaDto tarefaDto)
    {
        var tarefa = _mapper.Map<Tarefa>(tarefaDto);
        await _tarefaRepository.AdicionarTarefa(tarefa);
        return await _tarefaRepository.SalvarAlteracoesAsync();
    }

    public async Task<bool> AtualizarTarefa(int tarefaId, TarefaDto tarefaDto)
    {
        var tarefa = await _tarefaRepository.ObterTarefaPorId(tarefaId);
        if (tarefa == null) return false;

        _mapper.Map(tarefaDto, tarefa);
        return await _tarefaRepository.SalvarAlteracoesAsync();
    }

    public async Task<bool> RemoverTarefa(int tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterTarefaPorId(tarefaId);
        if (tarefa == null) return false;

        await _tarefaRepository.RemoverTarefa(tarefa);
        return await _tarefaRepository.SalvarAlteracoesAsync();
    }
}
