using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class MutationServiceUnitTests
    {
        private MutationService _mutationService = new MutationService();

        [Test]
        public void MutationService_Mutate_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => _mutationService.Mutate(new List<Individual>()));
        }

        [Test]
        public void MutationService_Mutate_ShouldMutate()
        {        
            var individuals = new List<Individual>() 
            {
                new Individual()
                {
                    Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8}
                }
            };

            var result = _mutationService.Mutate(individuals);

            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].Genotype.Count, Is.EqualTo(8));
        }

    }
}
