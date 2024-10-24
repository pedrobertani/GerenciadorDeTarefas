using Domain.Entities;

namespace Infrastructure.Data.TEMP_Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] HashPassword { get; set; } = null!;

    public byte[] SaltPassword { get; set; } = null!;

    public DateTime DateRegister { get; set; }

    public virtual ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
}
