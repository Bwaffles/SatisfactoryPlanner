using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Outbox
{
    internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", "users");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}