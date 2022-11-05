using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;

namespace TSP_Problem.Services
{
    public class TournamentSelectionService : ISelectionService
    {
        private int _numberOfTournament { get; set; }
        private int _sizeOfTournament { get; set; } = 2;

        private RandomGeneratorNumbersService _randomGeneratorNumbersService;
        

        public TournamentSelectionService(int numberOfTournament)
        {
            _numberOfTournament = numberOfTournament;           
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
        }

        public List<Individual> Select(List<Individual> individuals)
        {
            var tournamentResult = new List<Individual>();

            if(!individuals.Any())
                throw new ArgumentNullException(nameof(individuals));

            for(var i = 0; i < _numberOfTournament; i++) 
            {
                tournamentResult.Add(DoTournamentWithSizeTwo(individuals));          
            }

            return tournamentResult;
        }

        private Individual DoTournamentWithSizeTwo(List<Individual> individuals) 
        {           
            var firstIndex = _randomGeneratorNumbersService.GetInt(0, _numberOfTournament);
            var secondIndex = _randomGeneratorNumbersService.GetInt(0, _numberOfTournament);

            var firstIndividual = individuals[firstIndex];
            var secondIndividual = individuals[secondIndex];

            if (firstIndividual.Distance > secondIndividual.Distance)
                return firstIndividual;

            return secondIndividual;

        }

    }
}
