namespace Application.DTOs;

public class TarefaDto
{
    public int TarefaId { get; set; }  

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public bool Concluida { get; set; } = false;

    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public int UsuarioId { get; set; }
}
