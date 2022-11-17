// See https://aka.ms/new-console-template for more information
using TSP_Problem.Abstractions;
using TSP_Problem.Services;
using TSP_Problem_Common.Models;
using TSP_Visualization;


//parameters algorithm
var cityFile = "data_131.txt";
var initialNumberPopulation = 10;
var numberMaxCities = 131;
var numberIterations = 100;
var numberExecutions = 10;

var LoadFileCitiesService = new LoadFileCitiesService();
var cities = LoadFileCitiesService.LoadFile(cityFile);
if(cities.Count != numberMaxCities) 
{
    throw new Exception("The count of loaded cities is diferent from numberMaxCities");
}

var worldData = new WorldData() 
{ 
    Cities = cities,
    TotalCities = cities.Count,
};

ISaveGenerationService JsonSaveGenerationService = new JsonSaveGenerationService();

for (var i = 1; i <= numberExecutions; i++)
{
    Console.WriteLine("Execution: " + i);
    var GeneticAlgorithmService = new GeneticAlgorithmService(initialNumberPopulation, numberMaxCities, numberIterations, worldData);
    var finalPopulation = GeneticAlgorithmService.EvolveAlgorithm();
    JsonSaveGenerationService.SaveGenerationJson(i, finalPopulation);

    Console.WriteLine("Best Individual: " + finalPopulation.BestIndividual.Distance);
    Console.WriteLine("Generations: " + finalPopulation.Generations.Count);
    Console.WriteLine("------");
    Console.WriteLine();
}

//create plots
CreatePlot createPlot = new CreatePlot(numberExecutions, numberIterations);
createPlot.CreateProgressCurve();


