using Services.SFGame;
using Xunit;

namespace Services.Tests.SFGame
{
    public class SFGameServiceTests
    {
        /// <summary>
        /// Run this test to write the seed items with resources script.
        /// </summary>
        [Fact]
        public void SeedItemResources()
        {
            new SeedItemResourcesScriptWriter().Write();
        }
    }
}
