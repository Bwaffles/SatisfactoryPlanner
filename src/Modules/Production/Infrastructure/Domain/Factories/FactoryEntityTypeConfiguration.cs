﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisfactoryPlanner.Modules.Production.Domain.Factories;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Domain.Factories
{
    internal class FactoryEntityTypeConfiguration : IEntityTypeConfiguration<Factory>
    {
        public void Configure(EntityTypeBuilder<Factory> builder)
        {
            builder.ToTable("factories", "production");

            builder
                .HasKey(_ => _.Id);

            builder.Property<FactoryId>("Id").HasColumnName("id");
            builder.Property<string>("_name").HasColumnName("name");
            builder.Property<FactoryId>("_builtUnderFactoryId").HasColumnName("built_under_factory_id");
        }
    }
}