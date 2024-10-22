using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserDto
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha pode conter de 6 a 100 caracteres.")]
    public string Password { get; set; }
}
