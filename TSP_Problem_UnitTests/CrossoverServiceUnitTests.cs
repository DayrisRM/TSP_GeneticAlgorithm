using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem_Common.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class CrossoverServiceUnitTests
    {
        private const double _crossoverProbability = 1;
        CrossoverService _crossoverService = new(_crossoverProbability);

        [Test]
        public void CrossoverService_SelectWithEmptyParents_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => _crossoverService.SelectParentsAndCrossIfPossible(new List<Individual>()));
        }

        

    }
}
