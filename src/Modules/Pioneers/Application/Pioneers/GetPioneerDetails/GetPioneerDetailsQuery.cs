using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails
{
    public class GetPioneerDetailsQuery : QueryBase<PioneerDetailsDto?>
    {
        public Guid PioneerId { get; }

        public GetPioneerDetailsQuery(Guid pioneerId) => PioneerId = pioneerId;
    }
}