using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserTask
{
    [Key]
    public int TaskId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public bool Completed { get; set; } = false;

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User User { get; set; }
}
