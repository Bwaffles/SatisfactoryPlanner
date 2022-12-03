using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer
{
    public class SpawnPioneerCommand : InternalCommandBase
    {
        internal Guid PioneerId { get; }

        [JsonConstructor]
        public SpawnPioneerCommand(Guid id, Guid pioneerId) : base(id) => PioneerId = pioneerId;
    }
}