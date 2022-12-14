﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class MutationService
    {
        private double _mutationProbability { get; set; }

        private RandomGeneratorNumbersService _randomGeneratorNumbersService { get; set; }

        private SwapMutationService _swapMutationService { get; set; }

        public MutationService(double mutationProbability)
        {
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
            _swapMutationService = new SwapMutationService();
            _mutationProbability = mutationProbability;
        }

        public MutationService(RandomGeneratorNumbersService randomGeneratorNumbersService, SwapMutationService swapMutationService)
        {
            _randomGeneratorNumbersService = randomGeneratorNumbersService;
            _swapMutationService = swapMutationService;
        }



        public List<Individual> Mutate(List<Individual> individuals)
        {
            if (!individuals.Any())
                throw new ArgumentNullException(nameof(individuals));           

            foreach (var individual in individuals)
            {
                var p = _randomGeneratorNumbersService.GetDouble();

                if (p < _mutationProbability)
                {
                    _swapMutationService.Mutate(individual.Genotype);
                }

            }

            return individuals;
        }

    }
}
