using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using MainProject;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using OfficeOpenXml;


namespace MainProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleFacade();

            var dataManager = new DataManager();
            try
            {
                //excel.GetFactoriesFromExcel().GetAwaiter().GetResult();
                var task = dataManager.GetFactoriesFromExcel();
                task.Start();
                task.Wait();
                foreach (var factory in dataManager.Factories)
                {
                    factory.GetInfo();
                }

                var ids = dataManager.Units.Where(d => dataManager.Tanks.Select(x => x.UnitId).Contains(d.ID));
                var result = (from t in dataManager.Tanks
                              join u in dataManager.Units on t.UnitId equals u.ID
                              join f in dataManager.Factories on u.FactoryId equals f.ID
                              select new { Unit = u, Tank = t, Factory = f }).ToList();
                foreach (var r in result)
                {
                    console.WriteInConsole($"Название резервуара = {r.Tank.Name}, название цеха = {r.Unit.Name}, название фабрики = {r.Factory.Name}\n");
                }



                double sum = 0;
                foreach (var t in dataManager.Tanks)
                {
                    sum += t.Volume;
                }

                console.WriteInConsole($"Сумма загрузки всех резервуаров: {sum}");

                var search = new SearchCollection();
                search.SearchInCollection(dataManager);
                console.WriteInConsole("Для сериализации нажмите 1");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    var serialize = Serializer.Serialize((IReadOnlyCollection<Factory>)dataManager.Factories, (IReadOnlyCollection<Unit>)dataManager.Units, (IReadOnlyCollection<Tank>)dataManager.Tanks);
                    File.WriteAllText("struct.json", serialize);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"Возникло исключение {e.Message}");
            }


        }
    }
}