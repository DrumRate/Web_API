using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenMockDataApi.Repository.RandomGenerators
{
    public abstract class AbstractRandomGenerator
    {

    }
    public abstract class RandomGenerator<T> : AbstractRandomGenerator
    {
        public abstract T GetNext();

        public RandomGenerator<T> Skip(int skip)
        {
            for (var i = 0; i < skip; i++)
            {
                GetNext();
            }
            return this;
        }
        public IEnumerable<T> GetValues(int take)
        {
            var values = new List<T>();
            for (int i = 0; i < take; i++)
            {
                values.Add(GetNext());
            }
            return values;

        }
    }
}
