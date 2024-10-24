
namespace Infrastructure.Data.TEMP_Models;

public partial class UserTask
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Completed { get; set; }

    public DateTime DateCreated { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
