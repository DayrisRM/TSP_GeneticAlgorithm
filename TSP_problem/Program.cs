// See https://aka.ms/new-console-template for more information
using TSP_Problem.Models;
using TSP_Problem.Services;


//parameters algorithm
var cityFile = "data_131.txt";
var initialNumberPopulation = 10;
var numberMaxCities = 131;
var numberIterations = 100;

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


//generar initial population
//generar para cada individuo un genotipo que sea una lista de tamaño numberMaxCities con un Random de 0 a 131
var RandomPopulationInitializerService = new RandomPopulationInitializerService();
var initialPopulation = RandomPopulationInitializerService.Initialize(numberMaxCities, initialNumberPopulation);

//evaluar individuos de población
//función de fitness
var FitnessCalculatorService = new FitnessCalculatorService(worldData);
foreach (var ind in initialPopulation.Individuals) 
{
    FitnessCalculatorService.Evaluate(ind);
}

//selección de padres por torneo
var TournamentSelectionService = new TournamentSelectionService(initialNumberPopulation);
var tournamentResult = TournamentSelectionService.Select(initialPopulation.Individuals);

//cruce parcialmente mapeado
