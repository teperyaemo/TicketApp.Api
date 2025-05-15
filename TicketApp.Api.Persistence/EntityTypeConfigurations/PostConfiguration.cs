using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence.EntityTypeConfigurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(model => model.Id);
        builder.HasIndex(model => model.Id).IsUnique();
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();
    }
}