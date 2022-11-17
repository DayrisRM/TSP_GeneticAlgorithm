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
            string json = JsonConvert.SerializeObject(population);
            var fileName = $"{numberExecution}_population.json";
            var pathFile = @"../../../Data/generations/" + fileName;

            File.WriteAllText(pathFile, json);
        }
    }
}
