using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSP_Problem.Services;

namespace TSP_Problem_UnitTests
{
    public class RandomGeneratorNumbersServiceUnitTests
    {
        private RandomGeneratorNumbersService _randomGeneratorNumbersService = new RandomGeneratorNumbersService();

        [Test]
        public void GetUniqueInts_WithBadLength_ShouldThrowArgumentOutOfRangeException()
        {            
            var length = 131;
            var min = 1;
            var max = 131;
            Assert.Throws<ArgumentOutOfRangeException>(() => _randomGeneratorNumbersService.GetUniqueInts(length, min, max));
        }

        [Test]
        public void GetUniqueInts_WithCorrectParameters_ShouldReturnArray()
        {            
            var length = 131;
            var min = 1;
            var max = 132;
            var randomNumber = _randomGeneratorNumbersService.GetUniqueInts(length, min, max);
            Assert.IsNotNull(randomNumber);
            Assert.AreEqual(length, randomNumber.Length);
        }

        [Test]
        public void GetUniqueInts_WithCorrectParameters_ShouldHaveMinAndMaxValues()
        {
            var length = 131;
            var min = 1;
            var max = 132;
            var randomNumber = _randomGeneratorNumbersService.GetUniqueInts(length, min, max);
            Assert.IsTrue(randomNumber.Contains(min));
            Assert.IsTrue(randomNumber.Contains(length));
            Assert.IsFalse(randomNumber.Contains(0));
        }
    }
}
