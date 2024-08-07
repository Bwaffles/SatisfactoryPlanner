﻿using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.DataAccess
{
    /// <summary>
    ///     Dependency injection setup for Data Access of the Production module.
    ///     This registers the classes that handle the database connections for this module.
    ///     Should be called from the module Startup.
    /// </summary>
    internal class DataAccessModule : Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;

        internal DataAccessModule(string databaseConnectionString, ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register the database connection for executing queries
            builder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            // Register EF for executing commands
            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProductionContext>();
                    dbContextOptionsBuilder.UseNpgsql(_databaseConnectionString);
                    dbContextOptionsBuilder
                        .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                    return new ProductionContext(dbContextOptionsBuilder.Options, _loggerFactory);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            // Register all the Repositories in the Infrastructure assembly
            // Repositories are used for accessing the database through EF in commands
            builder.RegisterAssemblyTypes(Assemblies.Infrastructure)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}