using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class VAMMEvaluatorService : IEvaluator<List<Population>, double>
    {
        public double Evaluate(List<Population> populations)
        {           
            double sumOfDistance = 0;
            foreach (var population in populations) 
            {
                sumOfDistance += population.BestIndividual.Distance;
            }

            sumOfDistance /= populations.Count;

            return sumOfDistance;
        }
    }
}
