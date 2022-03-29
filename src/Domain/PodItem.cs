using System.Collections.Generic;

namespace Domain
{
    public class PodItem
    {
        /// <summary>
        /// The recipe used to produce this item.
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// The inputs required to produce this item.
        /// </summary>
        public ICollection<Input> Inputs { get; set; }

        /// <summary>
        /// The outputs required to produce this item.
        /// </summary>
        public ICollection<Output> Outputs { get; set; }
    }
}