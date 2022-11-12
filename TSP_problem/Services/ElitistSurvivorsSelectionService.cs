using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem.Models;

namespace TSP_Problem.Services
{
    public class ElitistSurvivorsSelectionService : ISurvivorsSelectionService
    {
        public List<Individual> SelectIndividuals(List<Individual> parents, List<Individual> children)
        {
            var bestParent = parents.OrderByDescending(x => x.Distance).Take(1).FirstOrDefault();
            var worstChildren = children.OrderBy(x => x.Distance).Take(1).FirstOrDefault();

            if(worstChildren.Distance < bestParent.Distance) 
            {
                children.Remove(worstChildren);
                children.Add(bestParent);
            }            
            
            return children;
        }
    }
}
