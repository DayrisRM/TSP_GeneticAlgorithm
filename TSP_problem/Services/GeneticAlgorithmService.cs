using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class GeneticAlgorithmService
    {
        //Parameters algorithm
        private int _initialNumberPopulation { get; set; }
        private int _numberMaxCities { get; set; }
        private int _numberIterations { get; set; }
        private WorldData _worldData { get; set; }
        private double _crossoverProbability { get; set; }
        private double _mutationProbability { get; set; }

        //Services
        private IPopulationInitializerService PopulationInitializerService { get; set; }
        private IFitnessCalculator FitnessCalculatorService { get; set; }
        private ISelectionService TournamentSelectionService { get; set; }
        private CrossoverService CrossoverService { get; set; }
        private MutationService MutationService { get; set; }
        private ISurvivorsSelectionService ElitistSurvivorsSelectionService { get; set; }
        private IPopulationService PopulationService { get; set; }



        public GeneticAlgorithmService(int initialNumberPopulation, int numberMaxCities, int numberIterations, WorldData worldData, double crossoverProbability, double mutationProbability)
        {
            _initialNumberPopulation = initialNumberPopulation > 0 ? initialNumberPopulation : throw new ArgumentOutOfRangeException(nameof(initialNumberPopulation));
            _numberMaxCities = numberMaxCities > 0 ? numberMaxCities : throw new ArgumentOutOfRangeException(nameof(numberMaxCities));
            _numberIterations = numberIterations > 0 ? numberIterations : throw new ArgumentOutOfRangeException(nameof(numberIterations));
            _worldData = worldData ?? throw new ArgumentNullException(nameof(worldData));
            _crossoverProbability = crossoverProbability;
            _mutationProbability = mutationProbability;

            PopulationInitializerService = new RandomPopulationInitializerService();
            FitnessCalculatorService = new FitnessCalculatorService(_worldData);
            TournamentSelectionService = new TournamentSelectionService(_initialNumberPopulation);
            CrossoverService = new CrossoverService(_crossoverProbability);
            MutationService = new MutationService(_mutationProbability);
            ElitistSurvivorsSelectionService = new ElitistSurvivorsSelectionService();
            PopulationService = new PopulationService();            
        }

        public Population EvolveAlgorithm() 
        {
            //Initialize population
            var population = PopulationInitializerService.Initialize(_numberMaxCities, _initialNumberPopulation);

            //Evaluate population
            CalculateFitness(population.CurrentGeneration.Individuals);

            //bucle
            var actualIteration = 1;
            while (actualIteration <= _numberIterations) 
            {
                //select parents by tournament              
                var tournamentResult = TournamentSelectionService.Select(population.CurrentGeneration.Individuals);

                //cross parents by partially mapped
                var crossResult = CrossoverService.SelectParentsAndCrossIfPossible(tournamentResult);

                //mutate using swap mutation
                var mutatedElements = MutationService.Mutate(crossResult);

                //evaluate mutated elements
                CalculateFitness(mutatedElements);

                //select survivors
                var newIndividuals = ElitistSurvivorsSelectionService.SelectIndividuals(population.CurrentGeneration.Individuals, mutatedElements);

                //add new generation to population
                PopulationService.CreateNewGeneration(population, newIndividuals);
                actualIteration++;
            }
            //return population. we will show the best individual
            return population;
        }

        private void CalculateFitness(List<Individual> individuals) 
        {
            foreach (var ind in individuals)
            {
                FitnessCalculatorService.Evaluate(ind);
            }
        }
        

    }
}
