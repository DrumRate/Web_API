using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JSONParse
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\dnevn\\Downloads\\JSON.json";
            var file = File.ReadAllText(path);
            IEnumerable<Structures> structures = JsonSerializer.Deserialize<Structures[]>(file);
            var numOfDeal = GetNumbersOfDeals(structures);
            Print(structures);
            PrintId(numOfDeal);
        }

        public static IList<string> GetNumbersOfDeals(IEnumerable<Structures> deals)
        {
            var result = deals.Where(d => d.Sum >= 100)
                                       .OrderBy(d => d.Date)
                                       .Take(3)
                                       .OrderByDescending(d => d.Sum)
                                       .Select(d => d.Id)
                                       .ToList<string>();
            return result;
        }

        public static void Print(IEnumerable<Structures> deals)
        {
            foreach (var deal in deals)
            {
                Console.WriteLine($"Id: {deal.Id} Sum = {deal.Sum} Data: {deal.Date}");
            }
        }

        public static void PrintId(IEnumerable<string> numOfDeal)
        {
            foreach (var num in numOfDeal)
            {
                Console.WriteLine($"Id = {num}");
            }

        }
    }
}
