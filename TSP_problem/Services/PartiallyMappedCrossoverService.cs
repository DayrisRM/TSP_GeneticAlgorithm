using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Abstractions;
using TSP_Problem_Common.Models;

namespace TSP_Problem.Services
{
    public class PartiallyMappedCrossoverService : ICrossoverService
    {
        private RandomGeneratorNumbersService _randomGeneratorNumbersService { get; set; }

        public PartiallyMappedCrossoverService()
        {
            _randomGeneratorNumbersService = new RandomGeneratorNumbersService();
        }

        public PartiallyMappedCrossoverService(RandomGeneratorNumbersService randomGeneratorNumbersService)
        {
            _randomGeneratorNumbersService = randomGeneratorNumbersService;
        }

        public List<Individual> Cross(List<Individual> parents)
        {
            var descendants = new List<Individual>();

            var parent1 = parents[0];
            var parent2 = parents[1];
            var size = parent1.Genotype.Count;

            //select segment positions of chromosome
            var segmentIndices = GetSegmentIndices(size);
            var firstCutPoint = segmentIndices.Item1;
            var secondCutPoint = segmentIndices.Item2;

            //Cut segment
            var parent1CuttedSegment = GetSegmentFromList(parent1.Genotype, firstCutPoint, secondCutPoint);
            var parent2CuttedSegment = GetSegmentFromList(parent2.Genotype, firstCutPoint, secondCutPoint);

            //create childs with the segment of the parent
            var child2 = CreateEmptyGenotypeWithSegment(size, firstCutPoint, secondCutPoint, parent1CuttedSegment);
            var child1 = CreateEmptyGenotypeWithSegment(size, firstCutPoint, secondCutPoint, parent2CuttedSegment);

            //
            for (int i = 0; i < size; i++)
            {
                if (i >= firstCutPoint && i <= secondCutPoint)
                {
                    continue;
                }

                var geneForChild1 = GetGeneNotInMappingSection(parent1.Genotype[i], parent2CuttedSegment, parent1CuttedSegment);
                ReplaceGene(i, geneForChild1, child1);

                var geneForChild2 = GetGeneNotInMappingSection(parent2.Genotype[i], parent1CuttedSegment, parent2CuttedSegment);
                ReplaceGene(i, geneForChild2, child2);
            }

            descendants.Add(new Individual() { Genotype = child1 });
            descendants.Add(new Individual() { Genotype = child2 });

            return descendants;
        }

        public List<int> CreateEmptyGenotypeWithSegment(int size, int firstCutPoint, int secondCutPoint, List<int> segment)
        {
            //var newGenotype = new List<int>(new int[size]);
            //newGenotype.RemoveRange(firstCutPoint, secondCutPoint - firstCutPoint);
            //newGenotype.InsertRange(firstCutPoint, segment);
            var realSize = size - 1;
            var gen1 = new List<int>(new int[firstCutPoint]);
            var gen2 = new List<int>(new int[realSize - secondCutPoint]);

            var newGenotype = new List<int>();
            newGenotype.AddRange(gen1);
            newGenotype.AddRange(segment);
            newGenotype.AddRange(gen2);

            return newGenotype;
        }

        private Tuple<int, int> GetSegmentIndices(int countGenotype) 
        {
            var indices = _randomGeneratorNumbersService.GetUniqueInts(2, 0, countGenotype);
            Array.Sort(indices);
            
            return new Tuple<int, int>(indices[0], indices[1]);
        }

        private List<int> GetSegmentFromList(List<int> genotype, int firstCutPoint, int secondCutPoint) 
        {
            return genotype
                .Skip(firstCutPoint)
                .Take((secondCutPoint - firstCutPoint) + 1)
                .ToList();
        }
        
        private int GetGeneNotInMappingSection(int candidateGene, List<int> mappingSection, List<int> otherParentMappingSection)
        {
            var indexOnMappingSection = mappingSection
                .Select((item, index) => new { Gene = item, Index = index })
                .FirstOrDefault(g => g.Gene.Equals(candidateGene));

            if (indexOnMappingSection != null)
            {
                return GetGeneNotInMappingSection(otherParentMappingSection[indexOnMappingSection.Index], mappingSection, otherParentMappingSection);
            }

            return candidateGene;
        }
        
        private void ReplaceGene(int position, int value, List<int> genotype) 
        {
            if (position > genotype.Count)
                throw new ArgumentException("Replace gen with invalid position.");

            genotype[position] = value;
        }
    
    }
}
