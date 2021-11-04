using Services.SFGame;
using System.Collections.Generic;

namespace Application.Pods.Queries.Create
{
    public class Item
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; internal set; }
    }

    public class CreatePodModel
    {
        public IEnumerable<Item> Items { get; internal set; }
    }
}
