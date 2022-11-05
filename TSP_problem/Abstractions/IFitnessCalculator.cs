using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Models;

namespace TSP_Problem.Abstractions
{
    public interface IFitnessCalculator
    {
        Individual Evaluate(Individual individual);
    }
}
