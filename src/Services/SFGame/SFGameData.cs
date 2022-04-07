using System.Collections.Generic;

namespace Services.SFGame
{
    public class SFGameData
    {
        public List<Item> Items { get; set; }
        public List<Schematic> Schematics { get; set; }
        public IEnumerable<Recipe> Recipes { get; internal set; }

        public SFGameData()
        {
            Items = new List<Item>();
            Schematics = new List<Schematic>();
            Recipes = new List<Recipe>();
        }
    }
}
