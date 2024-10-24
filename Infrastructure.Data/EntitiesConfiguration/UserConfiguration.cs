using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId)
               .HasName("PK__User__1788CC4C0E57A6E2");

        builder.ToTable("User");

        builder.Property(e => e.UserName)
               .HasMaxLength(50);
    }
}