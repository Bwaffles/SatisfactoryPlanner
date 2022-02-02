using System.Collections.Generic;

namespace Domain
{
    public class Output
    {
        /// <summary>
        /// The system generated unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The product of this output.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// The number of items that are produced.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Items that are being exported to other <see cref="Input"/>s.
        /// </summary>
        public ICollection<Transfer> Exports { get; set; }
    }
}