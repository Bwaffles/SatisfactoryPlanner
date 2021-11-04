using Services.SFGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pods.Queries.Create
{
    public class CreatePodQuery
    {
        public CreatePodModel Execute()
        {
            var sfGameService = new SFGameService();
            var gameData = sfGameService.GetGameData();
            return new CreatePodModel
            {
                Items = gameData
                .Items
                .Select(item => new Item { Category = item.Category, Code = item.ClassName, Name = item.DisplayName })
                .OrderBy(item => item.Name)
            };
        }
    }
}
