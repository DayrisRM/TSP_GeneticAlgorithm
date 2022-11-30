﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class CrossoverService
    {
        private const int NumberOfParents = 2;

        private double _crossoverProbability { get; set; }

        private ICrossoverService _partiallyMappedCrossoverService { get; set; }

        private RandomGeneratorNumbersService _randomGeneratorNumbersService { get; set; }

        public CrossoverService(double crossoverProbability)
        {
            _partiallyMappedCrossoverService = new PartiallyMappedCrossoverService();
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
            _crossoverProbability = crossoverProbability;
        }

        public List<Individual> SelectParentsAndCrossIfPossible(List<Individual> parents) 
        { 
            var chields = new List<Individual>();

            if(!parents.Any())
                throw new ArgumentNullException(nameof(parents));

            for (var i = 0; i < parents.Count; i += NumberOfParents) 
            {
                var parent1 = parents[i];
                var parent2 = parents[i + 1];

                //TODO: check if parents are equal. if equal do something

                var p = _randomGeneratorNumbersService.GetDouble();
                if(p <= _crossoverProbability) 
                {
                    var crossoverResult = _partiallyMappedCrossoverService.Cross(new List<Individual>() { parent1, parent2 });
                    chields.AddRange(crossoverResult);
                }
                else 
                {
                    //Include parents to chields
                    chields.Add(parent1);
                    chields.Add(parent2);
                }

            }

            return chields;
        }

    }
}
