using NUnit.Framework;
using TSP_Problem_Common.Models;
using TSP_Problem.Services;


namespace TSP_Problem_UnitTests
{
    public class RandomPopulationInitializerServiceUnitTests
    {        

        [Test]
        public void Initialize_WithCorrectParameters_ShouldHaveInitialNumberPopulation()
        {
            var randomPopulationInitializer = new RandomPopulationInitializerService();
            var numberMaxCities = 131;
            var initialNumberPopulation = 10;
            var randomPopulation = randomPopulationInitializer.Initialize(numberMaxCities, initialNumberPopulation);
            Assert.That(randomPopulation, Is.Not.Null);
            Assert.That(randomPopulation.CurrentGeneration.Individuals.Count, Is.EqualTo(initialNumberPopulation));
        }       
        

    }
}
