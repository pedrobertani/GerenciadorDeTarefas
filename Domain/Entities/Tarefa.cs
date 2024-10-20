using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Tarefa
{
    public int TarefaId { get; set; }

    [Required]
    [StringLength(100)]
    public string Titulo { get; set; }

    [StringLength(500)]
    public string Descricao { get; set; }

    public bool Concluida { get; set; } = false;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}
