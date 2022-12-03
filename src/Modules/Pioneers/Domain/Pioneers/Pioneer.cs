using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers
{
    public class Pioneer : Entity, IAggregateRoot
    {
        /// <summary>
        ///     The unique id of the pioneer.
        /// </summary>
        public PioneerId Id { get; }

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used by EF")]
        private Pioneer() { }

        private Pioneer(Guid pioneerId)
        {
            Id = new PioneerId(pioneerId);

            // TODO PioneerCreateDomainEvent
        }

        /// <summary>
        ///     Spawn a new pioneer. Get to work and be effective.
        /// </summary>
        /// <returns>Returns a newly spawned <see cref="Pioneer" />.</returns>
        public static Pioneer Spawn(Guid pioneerId)
            => new(pioneerId);
    }
}