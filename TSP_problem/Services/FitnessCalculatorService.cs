using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;

namespace TSP_Problem.Services
{
    public class FitnessCalculatorService : IFitnessCalculator
    {
        private WorldData _worldData { get; set; }

        public FitnessCalculatorService(WorldData worldData)
        {
            _worldData = worldData ?? throw new ArgumentNullException(nameof(worldData));
        }

        public Individual Evaluate(Individual individual)
        {
            double distance = 0;

            var genotype = individual.Genotype;

            if (!genotype.Any())
                throw new Exception("Genotype must be valid.");

            var firstCityInGenotype = GetCityById(genotype.First());

            var lastCityVisited = firstCityInGenotype;

            //Calculate value of the route
            foreach (var genotypeItem in genotype) 
            {
                try 
                {
                    var cityToVisit = GetCityById(genotypeItem);
                    distance += CalculateDistance(lastCityVisited, cityToVisit);
                    lastCityVisited = cityToVisit;
                }
                catch(Exception e) 
                {
                    var tt = e.Message;
                }
                
            }

            //Add to the route the conection between lastCity and FirstCity
            distance += CalculateDistance(lastCityVisited, firstCityInGenotype);
            
            individual.Distance = distance;

            return individual;
        }

        public static double CalculateDistance(City initialCity, City finalCity)
        {
            return Math.Sqrt(
                Math.Pow(finalCity.X - initialCity.X, 2) + 
                Math.Pow(finalCity.Y - initialCity.Y, 2)
                );
        }

        private City GetCityById(int cityId) 
            => _worldData.Cities.Where(x => x.Id == cityId).SingleOrDefault();

    }
}
