using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DatabaseMigrator.Scripts.Generators.SeedResourceExtractors
{
    internal class SeedResourceExtractorsScriptGenerator
    {
        internal void Generate()
        {
            var fileContents = File.ReadAllText($"{Scripts.GeneratorsPath}/SeedResourceExtractors/FactoryGame.FGBuildableResourceExtractor.json");
            var resourceExtractors = JsonConvert.DeserializeObject<List<ResourceExtractorDto>>(fileContents);

            var insertScriptBuilder = new StringBuilder();
            insertScriptBuilder.AppendLine("INSERT INTO factories.resource_extractors (code, " +
                                                                                      "name, " +
                                                                                      "description, " +
                                                                                      "extract_cycle_time, " +
                                                                                      "items_per_cycle, " +
                                                                                      "power_consumption, " +
                                                                                      "power_consumption_exponent, " +
                                                                                      "min_clockspeed, " +
                                                                                      "max_clockspeed, " +
                                                                                      "max_clockspeed_per_shard, " +
                                                                                      "max_shards" +
                                                                                      ") ");
            insertScriptBuilder.AppendLine("VALUES ");

            foreach (var resourceExtractor in resourceExtractors)
            {
                insertScriptBuilder.AppendLine($"('{resourceExtractor.ClassName}', " +
                                                $"'{resourceExtractor.DisplayName}', " +
                                                $"'{Escape(resourceExtractor.Description)}', " +
                                                $"{resourceExtractor.ExtractCycleTime}, " +
                                                $"{resourceExtractor.ItemsPerCycle}, " +
                                                $"{resourceExtractor.PowerConsumption}, " +
                                                $"{resourceExtractor.PowerConsumptionExponent}, " +
                                                $"{resourceExtractor.MinPotential}, " +
                                                $"{resourceExtractor.MaxPotential}, " +
                                                $"{resourceExtractor.MaxPotentialIncreasePerCrystal}, " +
                                                $"3" +
                                                $"),");
            }

            var insertScript = insertScriptBuilder.ToString().TrimEnd().TrimEnd(',');
            insertScript += ";";

            File.WriteAllText(Scripts.SeedResourceExtractors, insertScript);
        }

        internal void Delete()
        {
            File.Delete(Scripts.SeedResourceExtractors);
        }

        private string Escape(string value)
        {
            return value
                .Replace("'", "''")
                .Replace("\r\n", "\\r\\n");
        }
    }
}
