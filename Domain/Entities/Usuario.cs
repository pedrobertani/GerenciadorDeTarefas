using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Usuario
{
    public int UsuarioId { get; set; }

    [Required]
    [StringLength(50)]
    public string NomeUsuario { get; set; }

    [Required]
    public byte[] SenhaHash { get; set; }

    [Required]
    public byte[] SenhaSalt { get; set; }

    public DateTime DataRegistro { get; set; } = DateTime.Now; 
}
