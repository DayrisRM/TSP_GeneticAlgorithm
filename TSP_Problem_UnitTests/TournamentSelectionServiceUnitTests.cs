using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class TournamentSelectionServiceUnitTests
    {
        private TournamentSelectionService _tournamentSelectionService = new(10);

        [Test]
        public void TournamentSelectionService_SelectWithEmptyIndividuals_ShouldThrow()
        {            
            Assert.Throws<ArgumentNullException>(() => _tournamentSelectionService.Select(new List<Individual>()));
        }

        [Test]
        public void TournamentSelectionService_Select_ShouldReturnIndividuals()
        {
            var individuals = InitializeIndividues();
            var tournamentResult = _tournamentSelectionService.Select(individuals);
            Assert.NotNull(tournamentResult);
            Assert.IsTrue(tournamentResult.Count == 10);
        }

        private List<Individual> InitializeIndividues() 
        {
            var individues = new List<Individual>() 
            {
                new Individual()
                {
                    Id = 1,
                    Genotype = new List<int>() { 1, 2, 3, 4 },
                    Distance = 100
                },
                new Individual()
                {
                    Id = 2,
                    Genotype = new List<int>() { 4, 3, 2, 1 },
                    Distance = 120
                },
                new Individual()
                {
                    Id = 3,
                    Genotype = new List<int>() { 1, 3, 2, 4 },
                    Distance = 130
                },
                new Individual()
                {
                    Id = 4,
                    Genotype = new List<int>() { 1, 2, 3, 4 },
                    Distance = 134
                },
                new Individual()
                {
                    Id = 5,
                    Genotype = new List<int>() { 1, 4, 3, 2 },
                    Distance = 156
                },
                new Individual()
                {
                    Id = 6,
                    Genotype = new List<int>() { 1, 3, 4, 2 },
                    Distance = 186
                },
                new Individual()
                {
                    Id = 7,
                    Genotype = new List<int>() { 2, 3, 4, 1 },
                    Distance = 196
                },
                new Individual()
                {
                    Id = 8,
                    Genotype = new List<int>() { 2, 1, 4, 3 },
                    Distance = 166
                },
                new Individual()
                {
                    Id = 9,
                    Genotype = new List<int>() { 2, 4, 1, 3 },
                    Distance = 126
                },
                new Individual()
                {
                    Id = 10,
                    Genotype = new List<int>() { 2, 1, 4, 3 },
                    Distance = 116
                },

        };

            return individues;
        }

    }
}
