using Services.SFGame.Models.DocExtraction;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Services.SFGame
{
    public class SeedItemResourcesScriptWriter
    {
        /// <summary>
        ///     Write a script to seed the item table with items from the game files.
        /// </summary>
        public void Write()
        {
            var sut = new SFGameService();
            var gameData = sut.GetGameData();

            var insertScriptBuilder = new StringBuilder();
            insertScriptBuilder.AppendLine("INSERT INTO factories.items (type, " +
                                                                        "code, " +
                                                                        "name, " +
                                                                        "description, " +
                                                                        "stack_size, " +
                                                                        "can_be_deleted, " +
                                                                        "energy_value, " +
                                                                        "radioactive_decay, " +
                                                                        "form, " +
                                                                        "resource_sink_points) ");
            insertScriptBuilder.AppendLine("VALUES ");

            foreach (var item in gameData.Items
                .Where(_ => _.Type == ItemType.Resource)
                .OrderBy(_ => _.ResourceSinkPoints))
            {
                insertScriptBuilder.AppendLine($"('{item.Type}', " +
                                                $"'{item.ClassName}', " +
                                                $"'{item.DisplayName}', " +
                                                $"'{Escape(item.Description)}', " +
                                                $"{TransfromStackSize(item.StackSize)}, " +
                                                $"'{item.CanBeDiscarded}', " +
                                                $"{TransformEnergyValue(item.EnergyValue)}, " +
                                                $"{TransformRadioactiveDecay(item.RadioactiveDecay)}, " +
                                                $"'{TransformForm(item.Form)}', " +
                                                $"{TransformResourceSinkPoints(item.ResourceSinkPoints)}),");
            }

            var insertScript = insertScriptBuilder.ToString()
                .TrimEnd()
                .TrimEnd(',');
            insertScript += ";";

            File.WriteAllText("E:/Projects/SatisfactoryPlanner/src/Database/DatabaseMigrator/Scripts/0001__seed_items_with_resources.sql", insertScript);
        }

        private string TransformResourceSinkPoints(long resourceSinkPoints)
        {
            if (resourceSinkPoints == 0)
            {
                return "NULL";
            }

            return resourceSinkPoints.ToString();
        }

        private string TransformForm(ResourceForm form)
        {
            switch (form)
            {
                case ResourceForm.RF_SOLID:
                    return "Solid";
                case ResourceForm.RF_LIQUID:
                    return "Liquid";
                case ResourceForm.RF_GAS:
                    return "Gas";
                case ResourceForm.RF_HEAT:
                    return "Heat"; // ???
                default:
                    throw new InvalidOperationException($"Can't transfrom form of {form}");
            }
        }

        private string Escape(string value)
        {
            return value
                .Replace("'", "''")
                .Replace("\r\n", "\\r\\n");
        }

        private string TransformRadioactiveDecay(decimal radioactiveDecay)
        {
            if (radioactiveDecay == 0)
            {
                return "NULL";
            }

            return radioactiveDecay.ToString();
        }

        private string TransformEnergyValue(decimal energyValue)
        {
            if (energyValue == 0)
            {
                return "NULL";
            }

            return energyValue.ToString();
        }

        /// <summary>
        /// Transform the stack size from the game file enum to what we'll be storing it as in our database.
        /// </summary>
        private string TransfromStackSize(StackSize stackSize)
        {
            switch (stackSize)
            {
                case StackSize.UNKNOWN:
                case StackSize.SS_FLUID: // considering fluid as no stack size since you can't hold a liquid in your pocket
                    return "NULL";
                case StackSize.SS_ONE:
                    return "1";
                case StackSize.SS_SMALL:
                    return "50";
                case StackSize.SS_MEDIUM:
                    return "100";
                case StackSize.SS_BIG:
                    return "200";
                case StackSize.SS_HUGE:
                    return "500";
                default:
                    throw new InvalidOperationException($"Can't transfrom a stack size of {stackSize}");
            }
        }
    }
}