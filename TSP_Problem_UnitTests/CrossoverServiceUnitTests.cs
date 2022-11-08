using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class CrossoverServiceUnitTests
    {
        CrossoverService _crossoverService = new();

        [Test]
        public void CrossoverService_SelectWithEmptyParents_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => _crossoverService.SelectParentsAndCrossIfPossible(new List<Individual>()));
        }

        

    }
}
