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

        /// <summary>
        ///     The identifier of the pioneer in Auth0.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private string Auth0UserId { get; }

        [SuppressMessage("ReSharper", "UnusedMember.Local", Justification = "Used by EF")]
        private Pioneer() { }

        private Pioneer(string auth0UserId)
        {
            Id = new PioneerId(Guid.NewGuid());
            Auth0UserId = auth0UserId;
        }

        /// <summary>
        ///     Spawn a new pioneer. Get to work and be effective.
        /// </summary>
        /// <param name="auth0UserId">The identifier of the pioneer in Auth0.</param>
        /// <returns>Returns a newly spawned <see cref="Pioneer" />.</returns>
        public static Pioneer Spawn(string auth0UserId)
            => new(auth0UserId);
    }
}