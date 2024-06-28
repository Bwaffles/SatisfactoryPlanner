using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;

[method: JsonConstructor]
public class ExtractionRateDecreasedNotification(ExtractionRateDecreasedDomainEvent domainEvent) : DomainEventNotification<ExtractionRateDecreasedDomainEvent>(domainEvent);
