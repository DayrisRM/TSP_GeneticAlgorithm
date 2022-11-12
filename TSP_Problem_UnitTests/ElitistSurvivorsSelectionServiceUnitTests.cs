using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class ElitistSurvivorsSelectionServiceUnitTests
    {
        private ElitistSurvivorsSelectionService _elitistSurvivorsSelectionService = new();

        [Test]
        public void ElitistSurvivorsSelectionService_SelectIndividuals_ShouldReturnChildrenWithBestParent() 
        {
            var parents = new List<Individual>()
            {
                new Individual()
                {
                    Id = 1,
                    Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8},
                    Distance = 100
                },
                new Individual()
                {
                    Id= 2,
                    Genotype = new List<int>() { 1, 2, 8, 4, 5, 6, 7, 3},
                    Distance = 70
                },
                new Individual()
                {
                    Id = 3,
                    Genotype = new List<int>() { 3, 2, 8, 4, 5, 6, 7, 1},
                    Distance = 50
                }
            };

            var children = new List<Individual>()
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
                    Distance = 50
                }
            };

            var childrenWithElitist = _elitistSurvivorsSelectionService.SelectIndividuals(parents, children);

            Assert.That(childrenWithElitist.Count, Is.EqualTo(3));
            Assert.That(childrenWithElitist[0].Id, Is.EqualTo(4));
            Assert.That(childrenWithElitist[1].Id, Is.EqualTo(5));
            Assert.That(childrenWithElitist[2].Id, Is.EqualTo(1));
        }

        [Test]
        public void ElitistSurvivorsSelectionService_SelectIndividuals_ShouldReturnChildrenWithoutParentItem()
        {
            var parents = new List<Individual>()
            {
                new Individual()
                {
                    Id = 1,
                    Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8},
                    Distance = 80
                },
                new Individual()
                {
                    Id= 2,
                    Genotype = new List<int>() { 1, 2, 8, 4, 5, 6, 7, 3},
                    Distance = 70
                },
                new Individual()
                {
                    Id = 3,
                    Genotype = new List<int>() { 3, 2, 8, 4, 5, 6, 7, 1},
                    Distance = 50
                }
            };

            var children = new List<Individual>()
            {
                new Individual()
                {
                    Id = 4,
                    Genotype = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8},
                    Distance = 190
                },
                new Individual()
                {
                    Id = 5,
                    Genotype = new List<int>() { 1, 2, 8, 4, 5, 6, 7, 3},
                    Distance = 170
                },
                new Individual()
                {
                    Id = 6,
                    Genotype = new List<int>() { 3, 2, 8, 4, 5, 6, 7, 1},
                    Distance = 150
                }
            };

            var childrenWithElitist = _elitistSurvivorsSelectionService.SelectIndividuals(parents, children);

            Assert.That(childrenWithElitist.Count, Is.EqualTo(3));
            Assert.That(childrenWithElitist[0].Id, Is.EqualTo(4));
            Assert.That(childrenWithElitist[1].Id, Is.EqualTo(5));
            Assert.That(childrenWithElitist[2].Id, Is.EqualTo(6));
        }

    }
}
