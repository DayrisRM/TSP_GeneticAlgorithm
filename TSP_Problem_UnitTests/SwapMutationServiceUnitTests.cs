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
    public class SwapMutationServiceUnitTests
    {
        private SwapMutationService _swapMutationService = new SwapMutationService();

        [Test]
        public void SwapMutationService_Mutate_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => _swapMutationService.Mutate(new List<int>()));
        }

        [Test]
        public void SwapMutationService_Mutate_ShouldMutate()
        {
            var mockService = new Mock<RandomGeneratorNumbersService>();
            mockService
                .Setup(m => m.GetUniqueInts(2, 0, 8))
                .Returns(new int[] { 1, 3 });


            _swapMutationService = new SwapMutationService(mockService.Object);

            var h1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8};

            var m1 = _swapMutationService.Mutate(h1);

            Assert.That(m1, Is.Not.Null);
            Assert.That(m1.Count, Is.EqualTo(8));
            Assert.That(m1[1], Is.EqualTo(4));
            Assert.That(m1[3], Is.EqualTo(2));

        }

    }
}
