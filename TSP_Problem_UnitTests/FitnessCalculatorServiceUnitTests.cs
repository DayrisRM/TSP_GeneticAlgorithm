using NUnit.Framework;
using TSP_Problem.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class FitnessCalculatorServiceUnitTests
    {
        private FitnessCalculatorService _fitnessCalculator;
       
        [Test]
        public void FitnessCalculatorService_EvaluateWithEmptyGen_ShouldThrow()
        {
            InitializeService();
            Assert.Throws<Exception>(() => _fitnessCalculator.Evaluate(new Individual()));
        }

        [Test]
        public void FitnessCalculatorService_EvaluateWithValidGen_ShouldReturnValue()
        {
            InitializeService();
            var individual = new Individual()
            {
                Genotype = new List<int>() { 1, 2, 3, 4 }
            };
            
            individual = _fitnessCalculator.Evaluate(individual);
            Assert.IsNotNull(individual);
            Assert.Greater(individual.Distance, 0);
        }

        [Test]
        public void FitnessCalculatorService_CalculateDistance_ShouldReturnCorrectValue()
        {
            InitializeService();

            var cityStart = new City()
            {
                Id = 1,
                X = 0,
                Y = 2,
            };
            var cityEnd = new City()
            {
                Id = 2,
                X = 2,
                Y = 4,
            };
            
            var distance = FitnessCalculatorService.CalculateDistance(cityStart, cityEnd);
            var expectedValue = "2,828427";
            Assert.That(distance.ToString("#.######"), Is.EqualTo(expectedValue));
        }

        private void InitializeService()
        {
            var worldData = new WorldData();
            var cities = new List<City>() 
            {
                new City()
                {
                     Id = 1,
                     X = 0,
                     Y = 2,
                },
                new City()
                {
                     Id = 2,
                     X = 2,
                     Y = 4,
                },
                new City()
                {
                     Id = 3,
                     X = 4,
                     Y = 2,
                },
                new City()
                {
                     Id = 4,
                     X = 3,
                     Y = 0,
                }

            };
            
            worldData.Cities = cities;

            _fitnessCalculator = new FitnessCalculatorService(worldData);
        }
    }
}