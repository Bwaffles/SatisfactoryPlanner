namespace Domain
{
    public class Pod
    {
        /// <summary>
        /// The item that's being produced by this pod.
        /// </summary>
        public PodItem Item { get; set; }

        /// <summary>
        /// The user-defined number of the pod within its <see cref="ProductionLine"/>.
        /// Used for sorting and to help identify specific pods.
        /// </summary>
        /// <remarks>
        /// Perhaps this will be system generated but can be overridden by the user?
        /// </remarks>
        public int Number { get; set; }
    }
}