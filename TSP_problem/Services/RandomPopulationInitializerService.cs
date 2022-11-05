using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;

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
            var population = new Population();

            for (int i = 1; i <= initialNumberPopulation; i++) 
            {
                var citiesIndexes = _randomGeneratorNumbersService.GetUniqueInts(numberMaxCities, 1, numberMaxCities + 1);
                if(citiesIndexes == null) 
                {
                    throw new Exception("Cities indexes must not be null");
                }

                var individual = new Individual() 
                {
                    Id = i,
                    Genotype = citiesIndexes.ToList()
                };

                population.Individuals.Add(individual);

            }

            return population;
        }

    }
}
