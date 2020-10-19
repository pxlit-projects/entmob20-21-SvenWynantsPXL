using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StarWarsUniverse.Data.Repositories.Api;
using StarWarsUniverse.Domain;

namespace StarWarsUniverse.Tests
{
    [TestFixture]
    public class PlanetApiRepositoryTests
    {
        private PlanetApiRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new PlanetApiRepository();
        }

        [Test]
        public void GetAllPlanets()
        {
            IList<Planet> planets = _repo.GetAllPlanets();

            Assert.That(planets.Count, Is.GreaterThan(59));
        }
    }
}
