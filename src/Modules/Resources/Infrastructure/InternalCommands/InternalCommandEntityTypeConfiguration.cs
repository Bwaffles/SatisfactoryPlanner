using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.InternalCommands;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.InternalCommands
{
    internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("internal_commands", "resources");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedNever();
            builder.Property<string>("Type").HasColumnName("type");
            builder.Property<string>("Data").HasColumnName("data");
            builder.Property<DateTime?>("ProcessedDate").HasColumnName("processed_date");
        }
    }
}