using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor
{
    [method: JsonConstructor]
    public class ExtractorDismantledNotification(ExtractorDismantledDomainEvent domainEvent) : DomainEventNotification<ExtractorDismantledDomainEvent>(domainEvent);
}
