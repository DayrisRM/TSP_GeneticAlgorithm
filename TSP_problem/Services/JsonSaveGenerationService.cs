using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class JsonSaveGenerationService : ISaveGenerationService
    {
        public void SaveGenerationJson(int numberExecution, Population population)
        {
            var populationToSave = PreparePopulationToSave(population);
            string json = JsonConvert.SerializeObject(populationToSave);
            var fileName = $"{numberExecution}_population.json";
            var pathFile = @"../../../Data/generations/" + fileName;

            File.WriteAllTextAsync(pathFile, json);
        }

        private Population PreparePopulationToSave(Population population) 
        {            

            var populationToSave = new Population() 
            {
                CurrentGeneration = new Generation() 
                { 
                    GenerationNumber = population.CurrentGeneration.GenerationNumber, 
                    CreationDate = population.CurrentGeneration.CreationDate
                },                
                BestIndividual = new Individual()
                {                    
                    Distance = population.BestIndividual.Distance
                }
            };

            if(population.CurrentGeneration.BestIndividual != null) 
            {
                populationToSave.CurrentGeneration.BestIndividual = new Individual()
                {                    
                    Distance = population.CurrentGeneration.BestIndividual.Distance
                };
            }


            foreach (var generation in population.Generations) 
            {
                populationToSave.Generations.Add(new Generation()
                {
                    GenerationNumber = generation.GenerationNumber,
                    CreationDate = generation.CreationDate,
                    BestIndividual = new Individual() { Distance = generation.BestIndividual.Distance },
                });
            }

            return populationToSave;
        }

    }
}
