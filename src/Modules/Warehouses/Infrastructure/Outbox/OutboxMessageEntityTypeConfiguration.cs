using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_messages", "warehouses");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedNever();
            builder.Property<DateTime>("OccurredOn").HasColumnName("occurred_on");
            builder.Property<string>("Type").HasColumnName("type");
            builder.Property<string>("Data").HasColumnName("data");
            builder.Property<DateTime?>("ProcessedDate").HasColumnName("processed_date");
        }
    }
}