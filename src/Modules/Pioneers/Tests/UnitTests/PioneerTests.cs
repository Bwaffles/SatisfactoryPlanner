using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers.Rules;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.UnitTests
{
    [TestFixture]
    public class PioneerTests
    {
        [Test]
        public void SpawnPioneer_WithEmptyAuth0UserId_ThrowsException()
        {
            var pioneersCounter = Substitute.For<IPioneersCounter>();

            Action spawnPioneer = () => Pioneer.Spawn("", pioneersCounter);
            spawnPioneer.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SpawnPioneer_WithoutUniqueAuth0UserId_BreaksPioneerAuth0UserIdMustBeUniqueRule()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IPioneersCounter>();
            pioneersCounter.CountPioneersWithAuth0UserId(auth0UserId).Returns(x => 1);

            RuleAssertions.AssertBrokenRule<PioneerAuth0UserIdMustBeUniqueRule>(() =>
            {
                Pioneer.Spawn(auth0UserId, pioneersCounter);
            });
        }

        [Test]
        public void SpawnPioneer_WithUniqueAuth0UserId_IsSuccessful()
        {
            const string auth0UserId = "myAuth0UserId";

            var pioneersCounter = Substitute.For<IPioneersCounter>();
            pioneersCounter.CountPioneersWithAuth0UserId(auth0UserId).Returns(x => 0);

            Pioneer pioneer;
            Action spawnPioneer = () => pioneer = Pioneer.Spawn(auth0UserId, pioneersCounter);
            spawnPioneer.Should().NotThrow();
        }
    }
}