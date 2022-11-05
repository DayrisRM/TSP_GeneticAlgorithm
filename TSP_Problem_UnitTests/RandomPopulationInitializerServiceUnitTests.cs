using NUnit.Framework;
using TSP_Problem.Models;
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
            Assert.IsNotNull(randomPopulation);
            Assert.AreEqual(initialNumberPopulation, randomPopulation.Individuals.Count);
        }
        
        //TODO: Añadir test para comprobar que no añade varios individuos con los mismos valores

    }
}
