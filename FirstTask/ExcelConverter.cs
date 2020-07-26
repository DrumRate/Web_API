using System;
using System.Collections.Generic;
using System.Text;

namespace FirstTask
{
    static class ExcelConverter
    {
        public static IEnumerable<Factory> ConvertToFactory(IEnumerable<IEnumerable<string>> obj)
        {
            var result = new List<Factory>();
            foreach (List<string> row in obj)
            {
                result.Add(new Factory(Convert.ToInt32(row[0]), row[1], row[2]));
            }
            return result;
        }

        public static IEnumerable<Tank> ConvertToTank(IEnumerable<IEnumerable<string>> obj)
        {
            var result = new List<Tank>();
            foreach (List<string> row in obj)
            {
                result.Add(new Tank(Convert.ToInt32(row[0]), row[1], Convert.ToSingle(row[2]), Convert.ToSingle(row[3]), Convert.ToInt32(row[4])));
            }
            return result;
        }

        public static IEnumerable<Unit> ConvertToUnit(IEnumerable<IEnumerable<string>> obj)
        {
            var result = new List<Unit>();
            foreach (List<string> row in obj)
            {
                result.Add(new Unit(Convert.ToInt32(row[0]), row[1], Convert.ToInt32(row[2])));
            }
            return result;
        }
    }
}
