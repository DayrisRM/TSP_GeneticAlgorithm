using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Problem.Services
{
    public class RandomGeneratorNumbersService
    {
        private static readonly Random _Random = new();

        public int[] GetUniqueInts(int length, int min, int max)
        {
            var diff = max - min;

            if (diff < length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    $"The length is {length} - min {min} - max {max} - difference {diff}");
            }

            var orderedValues = Enumerable.Range(min, diff).ToList();
            var ints = new int[length];

            for (int i = 0; i < length; i++)
            {
                var removeIndex = GetInt(0, orderedValues.Count);
                ints[i] = orderedValues[removeIndex];
                orderedValues.RemoveAt(removeIndex);
            }

            return ints;
        }

        public int GetInt(int min, int max)
        {
            return _Random.Next(min, max);
        }
    }
}
