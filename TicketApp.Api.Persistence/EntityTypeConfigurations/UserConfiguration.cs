using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(model => model.Id);
        builder.HasIndex(model => model.Id).IsUnique();
        builder.HasMany(model => model.Tickets)
            .WithOne(loc => loc.User)
            .HasForeignKey(loc => loc.UserId);
        builder.HasMany(model => model.Posts)
            .WithOne(loc => loc.User)
            .HasForeignKey(loc => loc.UserId);
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();
    }
}