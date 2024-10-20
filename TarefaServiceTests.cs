using Application.Services;
using Domain.Entities;
using Domain.InterfacesRepositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class TarefaServiceTests
{
    private readonly Mock<ITarefaRepository> _tarefaRepositoryMock;
    private readonly TarefaService _tarefaService;

    public TarefaServiceTests()
    {
        _tarefaRepositoryMock = new Mock<ITarefaRepository>();
        _tarefaService = new TarefaService(_tarefaRepositoryMock.Object);
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

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefas(1, 1, 2))
            .ReturnsAsync(tarefasMock);

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

        _tarefaRepositoryMock.Setup(repo => repo.ObterTarefaPorId(1))
            .ReturnsAsync(tarefaMock);

        // Act
        var tarefa = await _tarefaService.ObterTarefaPorId(1);

        // Assert
        Assert.NotNull(tarefa);
        Assert.Equal(1, tarefa.TarefaId);
    }
}
