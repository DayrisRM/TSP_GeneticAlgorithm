﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;

namespace TSP_Problem.Services
{
    public class SwapMutationService : IMutationService<List<int>, List<int>>
    {        
        private RandomGeneratorNumbersService _randomGeneratorNumbersService { get; set; }

        public SwapMutationService()
        {
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
        }

        public SwapMutationService(RandomGeneratorNumbersService randomGeneratorNumbersService)
        {
            _randomGeneratorNumbersService = randomGeneratorNumbersService;
        }
        

        public List<int> Mutate(List<int> genotype) 
        {
            if (genotype.Count < 2)
                throw new ArgumentException("The genotype must have more than 2 genes.");

            var indices = _randomGeneratorNumbersService.GetUniqueInts(2, 0, genotype.Count);
            var firstIndice = indices[0];
            var secondIndice = indices[1];

            var firstIndiceValue = genotype[firstIndice];
            var secondIndiceValue = genotype[secondIndice];

            genotype[firstIndice] = secondIndiceValue;
            genotype[secondIndice] = firstIndiceValue;

            return genotype;

        }

    }
}
