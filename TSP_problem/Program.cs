using TSP_Problem.Abstractions;
using TSP_Problem.Services;
using TSP_Problem_Common.Models;
using TSP_Visualization;

//create type of executions
var simpleExecution = new ExecutionGA() 
{ 
    CityFile = "data_131.txt",
    InitialNumberPopulation = 50,
    NumberMaxCities = 131,
    NumberIterations = 1000,
    NumberExecutions = 10,
    FitnessBestSolution = 564,
};

var advanceExecution = new ExecutionGA() 
{
    CityFile = "data_10k.txt",
    InitialNumberPopulation = 10,
    NumberMaxCities = 131,
    NumberIterations = 100,
    NumberExecutions = 10
};


//Parameters of GA
Console.WriteLine("<---- Welcome to TSP Genetic Algorithm ---->");
Console.WriteLine();

Console.WriteLine("Choose option:");
Console.WriteLine($"A - Simple execution. ({simpleExecution.NumberMaxCities } cities - {simpleExecution.NumberIterations} iterations - {simpleExecution.NumberExecutions} executions - {simpleExecution.InitialNumberPopulation} initialNumberPopulation)");
Console.WriteLine($"B - Advance execution. ({advanceExecution.NumberMaxCities} cities - {advanceExecution.NumberIterations} iterations - {advanceExecution.NumberExecutions} executions - {advanceExecution.InitialNumberPopulation} initialNumberPopulation)");

var optionSelected = Console.ReadLine();

ExecutionGA parametersGA = new();

if(optionSelected.ToLower() == "a") 
{
    parametersGA = simpleExecution;
}
else if (optionSelected.ToLower() == "b")
{
    parametersGA = advanceExecution;
}
else 
{
    Console.WriteLine("Please select a correct option");
}


//TODO: crear una lista de combinaciones de probabilidades a probar.
//Add probabilities
parametersGA.MutationProbability = 1;
parametersGA.CrossoverProbability = 1;

//Load cities
var worldData = LoadCitiesFromFile(parametersGA);

//Initialize timer
var watch = System.Diagnostics.Stopwatch.StartNew();

//Call to GA
ISaveGenerationService JsonSaveGenerationService = new JsonSaveGenerationService();

for (var i = 1; i <= parametersGA.NumberExecutions; i++)
{
    Console.WriteLine("Execution: " + i);
    var GeneticAlgorithmService = new GeneticAlgorithmService(parametersGA.InitialNumberPopulation, parametersGA.NumberMaxCities, parametersGA.NumberIterations, worldData, parametersGA.CrossoverProbability, parametersGA.MutationProbability);
    var finalPopulation = GeneticAlgorithmService.EvolveAlgorithm();
    JsonSaveGenerationService.SaveGenerationJson(i, finalPopulation);

    Console.WriteLine("Best Individual: " + finalPopulation.BestIndividual.Distance);
    Console.WriteLine("Generations: " + finalPopulation.Generations.Count);
    Console.WriteLine("------");
    Console.WriteLine();
}

//Finish timer
watch.Stop();
var elapsedMs = watch.ElapsedMilliseconds;
var elapsedMinutes = TimeSpan.FromMilliseconds(elapsedMs).TotalMinutes;
Console.WriteLine($"Elapsed time in milliseconds:{elapsedMs}");
Console.WriteLine($"Elapsed time in minutes:{elapsedMinutes}");

//Get Saved execution-population
ILoadExecution<List<Population>> JsonLoadExecutionService = new JsonLoadExecutionService(parametersGA.NumberExecutions);
var savedPopulation = JsonLoadExecutionService.Load();

//Calculate VAMM
IEvaluator<List<Population>, double> VAMMEvaluatorService = new VAMMEvaluatorService();
var vammGA = VAMMEvaluatorService.Evaluate(savedPopulation);

Console.WriteLine($"VAMM:{vammGA}");

//Create Plots
Console.WriteLine("Generating plots...");
CreatePlot createPlot = new CreatePlot(parametersGA.NumberExecutions, parametersGA.NumberIterations, parametersGA.CrossoverProbability, parametersGA.MutationProbability);
createPlot.CreateProgressCurve(savedPopulation);

Console.WriteLine("Generated plots in /Data/figures");




//auxiliar functions
WorldData LoadCitiesFromFile(ExecutionGA parametersGA) 
{
    var LoadFileCitiesService = new LoadFileCitiesService();
    var cities = LoadFileCitiesService.LoadFile(parametersGA.CityFile);
    if (cities.Count != parametersGA.NumberMaxCities)
    {
        throw new Exception("The count of loaded cities is diferent from numberMaxCities");
    }

    var worldData = new WorldData()
    {
        Cities = cities,
        TotalCities = cities.Count,
    };

    return worldData;
}



public class ExecutionGA 
{
    public string CityFile { get; set; }
    public int InitialNumberPopulation { get; set; }
    public int NumberMaxCities { get; set; }
    public int NumberIterations { get; set; }
    public int NumberExecutions { get; set; }
    public int FitnessBestSolution { get; set; }
    public double MutationProbability { get; set; }
    public double CrossoverProbability { get; set; }
}