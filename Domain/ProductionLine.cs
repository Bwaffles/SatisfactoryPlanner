using System.Collections.Generic;

namespace Domain
{
    public class ProductionLine
    {
        /// <summary>
        /// The <see cref="Domain.Factory"/> that this <see cref="ProductionLine"/> belongs to.
        /// </summary>
        public Factory Factory { get; set; }

        /// <summary>
        /// User-defined custom name to identify the production line with.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The collection of <see cref="Pod"/>s that are used in this <see cref="ProductionLine"/>.
        /// </summary>
        public ICollection<Pod> Pods { get; set; }
    }
}
