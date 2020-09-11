using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMockDataApi.Repository.RandomGenerators
{
    public class StringRandomGenerator
    {
        private readonly Random _generator;
        private readonly string _basis = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public StringRandomGenerator(int seed)
        {
            _generator = new Random(seed);
        }

        public string GetNext()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < 15; i++)
            {
                var c = _basis[_generator.Next(0, _basis.Length)];
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();

        }
    }
}
