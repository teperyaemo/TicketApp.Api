using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence.EntityTypeConfigurations;

public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
{
    public void Configure(EntityTypeBuilder<Concert> builder)
    {
        builder.HasKey(model => model.Id);
        builder.HasIndex(model => model.Id).IsUnique();
        builder.HasMany(model => model.Tickets)
            .WithOne(loc => loc.Concert)
            .HasForeignKey(loc => loc.ConcertId)
            .IsRequired();
        builder.HasMany(model => model.Tickets)
            .WithOne(loc => loc.Concert)
            .HasForeignKey(loc => loc.ConcertId);
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();
    }
}