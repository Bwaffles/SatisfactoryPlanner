using Npgsql;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using System;
using System.Data;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure
{
    public class DbConnectionFactory : IDbConnectionFactory, IDisposable
    {
        private IDbConnection? _connection;
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateNewConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            // ReSharper disable once InvertIf
            if (_connection is not { State: ConnectionState.Open })
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }
        public void Dispose()
        {
            if (_connection is { State: ConnectionState.Open })
                _connection.Dispose();
        }
    }
}
