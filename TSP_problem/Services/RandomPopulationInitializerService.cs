using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class RandomPopulationInitializerService : IPopulationInitializerService
    {
        private RandomGeneratorNumbersService _randomGeneratorNumbersService;

        public RandomPopulationInitializerService()
        {
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
        }

        public Population Initialize(int numberMaxCities, int initialNumberPopulation)
        {
            var actualGeneration = new Generation
            {
                GenerationNumber = 1,
                CreationDate = DateTime.Now,
                Individuals = CreateIndividuals(numberMaxCities, initialNumberPopulation)
            };


            return new Population() { CurrentGeneration = actualGeneration };
        }

        private List<Individual> CreateIndividuals(int numberMaxCities, int initialNumberPopulation) 
        {
            var individuals = new List<Individual>();

            for (int i = 1; i <= initialNumberPopulation; i++)
            {
                var citiesIndexes = _randomGeneratorNumbersService.GetUniqueInts(numberMaxCities, 1, numberMaxCities + 1);
                if (citiesIndexes == null)
                {
                    throw new Exception("Cities indexes must not be null");
                }

                if (citiesIndexes.Distinct().Count() != numberMaxCities) 
                {
                    throw new Exception("Repeated cities in the initialization");
                }

                var individual = new Individual()
                {
                    Id = i,
                    Genotype = citiesIndexes.ToList()
                };

                individuals.Add(individual);
            }

            return individuals;
        }

    }
}
