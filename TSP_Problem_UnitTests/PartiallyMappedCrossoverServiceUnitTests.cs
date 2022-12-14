using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem_Common.Models;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class PartiallyMappedCrossoverServiceUnitTests
    {

        private PartiallyMappedCrossoverService _partiallyMappedCrossoverService = new PartiallyMappedCrossoverService();

        [Test]
        public void PartiallyMappedCrossoverServiceUnitTests_CreateEmptyGenotypeWithSegment_ShouldNotBeNull() 
        {
            var segment = new List<int>() { 3, 4, 5, 6 };
            var genotype = _partiallyMappedCrossoverService.CreateEmptyGenotypeWithSegment(size: 8, firstCutPoint: 2, secondCutPoint: 5, segment: segment);
            Assert.IsNotNull(genotype);
            Assert.IsTrue(8 == genotype.Count);
            Assert.AreEqual(genotype[1], 0);
            Assert.AreEqual(genotype[2], 3);
            Assert.AreEqual(genotype[3], 4);
            Assert.AreEqual(genotype[4], 5);
            Assert.AreEqual(genotype[5], 6);
        }

        [Test]
        public void PartiallyMappedCrossoverServiceUnitTests_Cross_ShouldCrossCorrectly() 
        {         
            var mockService = new Mock<RandomGeneratorNumbersService>();
            mockService
                .Setup(m => m.GetUniqueInts(2, 0, 8))
                .Returns(new int[] { 2, 5 });


            _partiallyMappedCrossoverService = new PartiallyMappedCrossoverService(mockService.Object);

            var parents = new List<Individual>()
            {
                new Individual()
                {
                    Genotype = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8}
                },
                new Individual()
                {
                    Genotype = new List<int>(){6, 5, 3, 1, 7, 4, 8, 2}
                }
            };
            var chields = _partiallyMappedCrossoverService.Cross(parents);
            Assert.IsNotNull(chields);
            Assert.IsTrue(2 == chields.Count);
            CheckChild1(chields[1].Genotype);
            CheckChild2(chields[0].Genotype);
        }

        private void CheckChild1(List<int> genotype) 
        {
            //17345682
            Assert.IsTrue(8 == genotype.Count);
            Assert.AreEqual(genotype[0], 1);
            Assert.AreEqual(genotype[1], 7);
            Assert.AreEqual(genotype[2], 3);
            Assert.AreEqual(genotype[3], 4);
            Assert.AreEqual(genotype[4], 5);
            Assert.AreEqual(genotype[5], 6);
            Assert.AreEqual(genotype[6], 8);
            Assert.AreEqual(genotype[7], 2);
        }
        private void CheckChild2(List<int> genotype)
        {
            //62317458
            Assert.IsTrue(8 == genotype.Count);
            Assert.AreEqual(genotype[0], 6);
            Assert.AreEqual(genotype[1], 2);
            Assert.AreEqual(genotype[2], 3);
            Assert.AreEqual(genotype[3], 1);
            Assert.AreEqual(genotype[4], 7);
            Assert.AreEqual(genotype[5], 4);
            Assert.AreEqual(genotype[6], 5);
            Assert.AreEqual(genotype[7], 8);
        }


    }
}
