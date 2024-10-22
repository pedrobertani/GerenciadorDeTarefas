using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class DataContext : DbContext
{
    public DataContext() { } 

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<UserTask> Task { get; set; }
}

