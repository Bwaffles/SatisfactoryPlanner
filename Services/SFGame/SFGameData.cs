using System.Collections.Generic;

namespace Services.SFGame
{
    public class SFGameData
    {
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Schematic> Schematics { get; set; }
        public IEnumerable<Recipe> Recipes { get; internal set; }
    }
}
