using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UserDto
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public byte[] HashPassword { get; set; }

    public byte[] SaltPassword { get; set; }

    public string Password { get; set; }

    public DateTime DateRegister { get; set; } = DateTime.Now;
}
