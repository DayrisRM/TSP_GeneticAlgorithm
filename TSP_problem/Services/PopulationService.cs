﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class PopulationService : IPopulationService
    {
        public void CreateNewGeneration(Population population, List<Individual> individuals)
        {
            if (!individuals.Any())
                throw new ArgumentNullException(nameof(individuals));

            //calculate best individual in current generation
            var bestIndividual = population.CurrentGeneration.Individuals.OrderBy(x => x.Distance).FirstOrDefault();
            population.CurrentGeneration.BestIndividual = bestIndividual;

            //insert current generation into the history of generations
            population.Generations.Add(population.CurrentGeneration);
            
            SelectBestIndividualInPopulation(bestIndividual, population);           

            //create new generation
            var newGeneration = new Generation() 
            {
                GenerationNumber = population.CurrentGeneration.GenerationNumber + 1,
                CreationDate = DateTime.Now,
                Individuals = individuals
            };

            population.CurrentGeneration = newGeneration;            
        }

        private void SelectBestIndividualInPopulation(Individual bestIndividualCurrentGeneration, Population population) 
        {
            //check if bestIndividual or generation is better than best individual of population
            if (population.BestIndividual == null || 
                bestIndividualCurrentGeneration.Distance < population.BestIndividual.Distance) 
            {
                population.BestIndividual = bestIndividualCurrentGeneration;
            }            
        }

    }
}
