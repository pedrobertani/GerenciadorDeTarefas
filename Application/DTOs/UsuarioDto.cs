namespace Application.DTOs;

public class UsuarioDto
{
    public int UsuarioId { get; set; }

    public string NomeUsuario { get; set; }

    public byte[] SenhaHash { get; set; }

    public byte[] SenhaSalt { get; set; }

    public string Senha { get; set; }

    public DateTime DataRegistro { get; set; } = DateTime.Now;
}
