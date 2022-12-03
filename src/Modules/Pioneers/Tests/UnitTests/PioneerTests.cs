using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.UnitTests
{
    [TestFixture]
    public class PioneerTests
    {
        [Test]
        public void SpawnPioneer_IsSuccessful()
        {
            var pioneerId = Guid.NewGuid();
            var pioneer = Pioneer.Spawn(pioneerId);
            
            pioneer.Id.Value.Should().Be(pioneerId);
        }
    }
}