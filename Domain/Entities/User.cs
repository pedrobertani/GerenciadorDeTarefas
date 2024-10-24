using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    public byte[] HashPassword { get; set; }

    [Required]
    public byte[] SaltPassword { get; set; }

    public DateTime DateRegister { get; set; } = DateTime.Now;
    public virtual ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();

}
