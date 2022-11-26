using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.GetPioneerDetails
{
    public class GetPioneerDetailsQuery : QueryBase<PioneerDetailsDto?>
    {
        public Guid PioneerId { get; }

        public GetPioneerDetailsQuery(Guid pioneerId) => PioneerId = pioneerId;
    }
}