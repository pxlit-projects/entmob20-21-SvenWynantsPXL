using NUnit.Framework;
using StarWarsUniverse.Data.Repositories.Api;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class PlanetApiRepositoryTests
    {
        private PlanetApiRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new PlanetApiRepository();
        }

        [Test]
        public void GetAllPlanetsShouldReturnEveryPlanet()
        {
            //Act
            var returnedPlanets = _repo.GetAllPlanets();

            //Assert
            Assert.That(returnedPlanets, Has.Count.GreaterThanOrEqualTo(60));
        }
    }
}