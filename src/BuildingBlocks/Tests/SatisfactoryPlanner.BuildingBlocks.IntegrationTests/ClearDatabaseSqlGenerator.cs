using System.Text;

namespace SatisfactoryPlanner.BuildingBlocks.IntegrationTests
{
    public class ClearDatabaseSqlGenerator(string schema)
    {
        private readonly string schema = schema;
        private readonly List<string> tables = [];

        public static ClearDatabaseSqlGenerator InSchema(string schema) => new(schema);

        public ClearDatabaseSqlGenerator ClearTable(string table)
        {
            tables.Add(table);
            return this;
        }

        public string GenerateSql()
        {
            var sql = new StringBuilder();
            foreach (var table in tables)
                sql.Append($"DELETE FROM {schema}.{table};");

            return sql.ToString();
        }
    }
}
