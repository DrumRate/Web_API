using GenMockDataApi.Repository.RandomGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenMockDataApi.Repository.RandomGeneratorFactory
{
    public class RandomGeneratorFactory : IRandomGeneratorFactory
    {
        public RandomGenerator<bool> CreateBoolean(int seed)
        {
            return new BoolRandomGenerator(seed);
        }

        public RandomGenerator<DateTime> CreateDateTime(int seed)
        {
            return new DateTimeRandomGenerator(seed);
        }

        public RandomGenerator<double> CreateNumeric(int seed)
        {
            return new DoubleRandomGenerator(seed);
        }

        public RandomGenerator<string> CreateString(int seed)
        {
            return new StringRandomGenerator(seed);
        }

    }
}
