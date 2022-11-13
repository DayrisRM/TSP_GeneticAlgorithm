using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class PopulationServiceUnitTests
    {
        private PopulationService _populationService = new();

        [Test]
        public void PopulationService_CreateNewGenerationWithEmptyIndividuals_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => _populationService.CreateNewGeneration(new Population(), new List<Individual>()));
        }

        [Test]
        public void PopulationService_CreateNewGeneration_ShouldCreateGeneration()
        {
            var population = new Population();
            var generation = new Generation() 
            {
                GenerationNumber = 1,
                CreationDate = DateTime.Now,
                Individuals = new List<Individual>() 
                {
                    new Individual()
                    {
                        Id = 1,
                        Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8},
                        Distance = 1100
                    },
                    new Individual()
                    {
                        Id= 2,
                        Genotype = new List<int>() { 1, 2, 8, 4, 5, 6, 7, 3},
                        Distance = 170
                    },
                    new Individual()
                    {
                        Id = 3,
                        Genotype = new List<int>() { 3, 2, 8, 4, 5, 6, 7, 1},
                        Distance = 150
                    }
                }
            };

            population.CurrentGeneration = generation;

            var newGenerationIndividuals = new List<Individual>()
            {
                new Individual()
                {
                    Id = 4,
                    Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8},
                    Distance = 90
                },
                new Individual()
                {
                    Id = 5,
                    Genotype = new List<int>() { 1, 2, 8, 4, 5, 6, 7, 3},
                    Distance = 70
                },
                new Individual()
                {
                    Id = 6,
                    Genotype = new List<int>() { 3, 2, 8, 4, 5, 6, 7, 1},
                    Distance = 150
                }
            };

            _populationService.CreateNewGeneration(population, newGenerationIndividuals);

            Assert.That(1, Is.EqualTo(population.Generations.Count));
            Assert.That(3, Is.EqualTo(population.Generations[0].BestIndividual.Id));
            Assert.That(1, Is.EqualTo(population.Generations[0].GenerationNumber));

            Assert.That(2, Is.EqualTo(population.CurrentGeneration.GenerationNumber));
            Assert.IsTrue(population.CurrentGeneration.Individuals.Any());

        }

    }
}
