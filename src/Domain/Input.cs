using System.Collections.Generic;

namespace Domain
{
    public class Input
    {
        /// <summary>
        /// The system generated unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ingredient that is consumed by this input.
        /// </summary>
        public Ingredient Ingredient { get; set; }

        /// <summary>
        /// The number of items that are consumed.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Items that are being imported to cover the <see cref="Amount"/> needed.
        /// </summary>
        public ICollection<Transfer> Imports { get; set; }
    }
}