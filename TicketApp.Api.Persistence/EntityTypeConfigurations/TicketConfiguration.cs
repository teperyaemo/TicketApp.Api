using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence.EntityTypeConfigurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(model => model.Id);
        builder.HasIndex(model => model.Id).IsUnique();
    }
}