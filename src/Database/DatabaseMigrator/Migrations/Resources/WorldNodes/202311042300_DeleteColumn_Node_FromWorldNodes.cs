using FluentMigrator;
using System;

namespace DatabaseMigrator.Migrations.Resources.WorldNodes
{
    [Migration(202311042300)]
    public class DeleteColumn_Node_FromWorldNodes : Migration
    {
        public override void Up()
        {
            Delete.Column("name").FromTable("world_nodes").InSchema("resources");
        }

        public override void Down()
        {
            Create.Column("name").OnTable("world_nodes").InSchema("resources")
                .AsString()
                .Nullable()
                .WithColumnDescription("A name to identify this extraction (e.g. Bauxite - Red Forest 1).");
        }
    }
}