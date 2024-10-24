using Domain.Entities;
using Infrastructure.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<UserTask> Task { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }

}
