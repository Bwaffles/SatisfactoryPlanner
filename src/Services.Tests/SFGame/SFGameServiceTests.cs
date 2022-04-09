using Services.SFGame;
using Xunit;

namespace Services.Tests.SFGame
{
    public class SFGameServiceTests
    {
        /// <summary>
        ///     Run this test to write the seed items with resources script.
        /// </summary>
        [Fact(Skip = "Seed")]
        public void SeedItemResources()
        {
            new SeedItemResourcesScriptWriter().Write();
        }

        /// <summary>
        ///     Run this test to write the seed resource nodes script.
        /// </summary>
        [Fact(Skip = "Seed")]
        public void SeedResourceNodes()
        {
            new SeedResourceNodeScriptWriter().Write();
        }
    }
}
