using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Problem.Models
{
    public class Population
    {
        public Generation CurrentGeneration { get; set; }

        public List<Generation> Generations { get; set; } = new List<Generation>();

        public Individual BestIndividual { get; set; }


        //delete this field after changes
        //public List<Individual> Individuals { get; set; } = new List<Individual>();
    }
}
