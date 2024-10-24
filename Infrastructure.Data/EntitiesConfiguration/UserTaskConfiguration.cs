namespace Infrastructure.Data.EntitiesConfiguration;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasKey(e => e.TaskId)
               .HasName("PK__Task__7C6949B102FE12C7");

        builder.ToTable("Task");

        builder.Property(e => e.Description)
               .HasMaxLength(500);

        builder.Property(e => e.Title)
               .HasMaxLength(100);

        builder.HasOne(d => d.User)
               .WithMany(p => p.Tasks)
               .HasForeignKey(d => d.UserId)
               .HasConstraintName("FK__Task__UserId__278EDA44");
    }
}
