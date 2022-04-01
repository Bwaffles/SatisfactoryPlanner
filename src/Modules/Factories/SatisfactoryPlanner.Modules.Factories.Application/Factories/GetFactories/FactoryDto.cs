using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.GetFactories
{
    public class FactoryDto
    {
        /// <summary>
        ///     The unique identifier for this factory.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The user-defined name that can be used to identify the factory.
        /// </summary>
        public string Name { get; set; }
    }
}
