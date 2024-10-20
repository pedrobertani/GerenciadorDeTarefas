using Application.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using Xunit;


namespace Tests;

public class TarefaServiceTests
{
    private readonly Mock<ITarefaRepository> _tarefaRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TarefaService _tarefaService;

    public TarefaServiceTests()
    {
        // Mocks para o repositório e o AutoMapper
        _tarefaRepositoryMock = new Mock<ITarefaRepository>();
        _mapperMock = new Mock<IMapper>();

        // Injetando os mocks no serviço
        _tarefaService = new TarefaService(_tarefaRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ObterTarefas_ShouldReturnTasks_WhenTasksExist()
    {
        // Arrange
        var tarefasMock = new List<Tarefa>
        {
            new Tarefa { TarefaId = 1, UsuarioId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1" },
            new Tarefa { TarefaId = 2, UsuarioId = 1, Titulo = "Tarefa 2", Descricao = "Descrição 2" }
        };

        var tarefasDtoMock = new List<TarefaDto>
        {
            new TarefaDto { TarefaId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1" },
            new TarefaDto { TarefaId = 2, Titulo = "Tarefa 2", Descricao = "Descrição 2" }
        };

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefas(1, 1, 2))
            .ReturnsAsync(tarefasMock);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<TarefaDto>>(tarefasMock))
            .Returns(tarefasDtoMock);

        // Act
        var tarefas = await _tarefaService.ObterTarefas(1, 1, 2);

        // Assert
        Assert.NotNull(tarefas);
        Assert.Equal(2, tarefas.Count());
    }

    [Fact]
    public async Task ObterTarefaPorId_ShouldReturnTask_WhenTaskExists()
    {
        // Arrange
        var tarefaMock = new Tarefa { TarefaId = 1, UsuarioId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1" };
        var tarefaDtoMock = new TarefaDto { TarefaId = 1, Titulo = "Tarefa 1", Descricao = "Descrição 1" };

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefaPorId(1))
            .ReturnsAsync(tarefaMock);

        _mapperMock.Setup(mapper => mapper.Map<TarefaDto>(tarefaMock))
            .Returns(tarefaDtoMock);

        // Act
        var tarefa = await _tarefaService.ObterTarefaPorId(1);

        // Assert
        Assert.NotNull(tarefa);
        Assert.Equal(1, tarefa.TarefaId);
    }

    [Fact]
    public async Task AdicionarTarefa_ShouldReturnTrue_WhenTaskIsAddedSuccessfully()
    {
        // Arrange
        var tarefaDto = new TarefaDto { TarefaId = 1, Titulo = "Nova Tarefa", Descricao = "Descrição da nova tarefa" };
        var tarefa = new Tarefa { TarefaId = 1, Titulo = "Nova Tarefa", Descricao = "Descrição da nova tarefa" };

        _mapperMock.Setup(mapper => mapper.Map<Tarefa>(tarefaDto))
            .Returns(tarefa);

        _tarefaRepositoryMock.Setup(repo => repo.AdicionarTarefa(tarefa)).Returns(Task.CompletedTask);
        _tarefaRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        var result = await _tarefaService.AdicionarTarefa(tarefaDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AtualizarTarefa_ShouldReturnTrue_WhenTaskIsUpdatedSuccessfully()
    {
        // Arrange
        var tarefaMock = new Tarefa { TarefaId = 1, Titulo = "Tarefa Existente", Descricao = "Descrição existente" };
        var tarefaDtoMock = new TarefaDto { TarefaId = 1, Titulo = "Tarefa Atualizada", Descricao = "Descrição atualizada" };

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefaPorId(1))
            .ReturnsAsync(tarefaMock);

        _mapperMock.Setup(mapper => mapper.Map(tarefaDtoMock, tarefaMock));

        _tarefaRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        var result = await _tarefaService.AtualizarTarefa(1, tarefaDtoMock);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoverTarefa_ShouldReturnTrue_WhenTaskIsDeletedSuccessfully()
    {
        // Arrange
        var tarefaMock = new Tarefa { TarefaId = 1, Titulo = "Tarefa a ser removida", Descricao = "Descrição" };

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefaPorId(1))
            .ReturnsAsync(tarefaMock);

        _tarefaRepositoryMock.Setup(repo => repo.RemoverTarefa(tarefaMock)).Returns(Task.CompletedTask);
        _tarefaRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        var result = await _tarefaService.RemoverTarefa(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoverTarefa_ShouldReturnFalse_WhenTaskDoesNotExist()
    {
        // Arrange
        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefaPorId(1))
            .ReturnsAsync((Tarefa)null);

        // Act
        var result = await _tarefaService.RemoverTarefa(1);

        // Assert
        Assert.False(result);
    }
}
