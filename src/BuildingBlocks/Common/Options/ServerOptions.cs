using System.ComponentModel.DataAnnotations;

namespace SatisfactoryPlanner.BuildingBlocks.Common.Options
{
    public sealed class ServerOptions
    {
        public const string ConfigurationSectionName = "Server";

        /// <summary>
        /// For example: *, localhost or a specific ipaddress. Default is *.
        /// </summary>
        public string? BindAddress { get; set; } = "*";

        [Range(0, 65535)]
        public int? Port { get; set; } = 55915;
    }
}
