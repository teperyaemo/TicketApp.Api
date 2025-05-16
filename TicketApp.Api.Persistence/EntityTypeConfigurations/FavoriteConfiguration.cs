using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence.EntityTypeConfigurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(model => model.Id);
        builder.HasIndex(model => model.Id).IsUnique();
    }
}