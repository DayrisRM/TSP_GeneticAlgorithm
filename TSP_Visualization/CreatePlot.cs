using Newtonsoft.Json;
using TSP_Problem_Common.Models;
using ScottPlot.Statistics;
using Population = TSP_Problem_Common.Models.Population;

namespace TSP_Visualization
{
    public class CreatePlot
    {
        private int _numberExecutions { get; set; }
        private int _numberIterations { get; set; }

        public CreatePlot(int numberExecutions, int numberIterations)
        {
            _numberExecutions = numberExecutions;
            _numberIterations = numberIterations;
        }       

        public void CreateProgressCurve(List<Population> savedPopulations) 
        {            

            if(savedPopulations.Count != _numberExecutions)
                throw new Exception("Generations invalid");

            var xCoor = new List<double>();
            var yCoor = new List<double>();

            for (int i = 1; i <= _numberIterations; i++) 
            {
                double sumOfDistance = 0;

                foreach (Population execution in savedPopulations) 
                {
                    var bestIndividual = execution.Generations[i - 1].BestIndividual;
                    sumOfDistance += bestIndividual.Distance;
                }

                double mean = sumOfDistance / _numberExecutions;
                xCoor.Add(i);
                yCoor.Add(mean);

            }

            var plt = new ScottPlot.Plot();
            plt.AddScatter(xCoor.ToArray(), yCoor.ToArray());

            plt.Title("Curva de progreso");
            plt.YLabel("Fitness");
            plt.XLabel("Generaciones");

            plt.SaveFig(@"../../../Data/figures/progress_curve.png");

        }
        
    }
}