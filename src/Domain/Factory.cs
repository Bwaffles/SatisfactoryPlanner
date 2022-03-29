using System;

namespace Domain
{
    public class Factory
    {
        /// <summary>
        /// The system generated unique id of the <see cref="Factory"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user-defined name of the <see cref="Factory"/>.
        /// </summary>
        public string Name { get; set; }
    }
}
