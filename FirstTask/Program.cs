using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OfficeOpenXml;


namespace FirstTask
{
    class Program
    {
        public static List<Factory> factories;
        public static List<Unit> units;
        public static List<Tank> tanks;
        static void Main(string[] args)
        {

            string path = @"C:\\Users\\dnevn\\Downloads\\Таблица_резервуаров.xlsx";
            var excel = new Excel();

            var factories = ExcelConverter.ConvertToFactory(excel.readExcel(path, 0)).ToList();
            var units = ExcelConverter.ConvertToUnit(excel.readExcel(path, 1)).ToList();
            var tanks = ExcelConverter.ConvertToTank(excel.readExcel(path, 2)).ToList(); 

            foreach (var factory in factories)
            {
                factory.GetInfo();
            }

            var ids = units.Where(d => tanks.Select(x => x.UnitId).Contains(d.ID));
            var result = (from u in units
                          join t in tanks on u.ID equals t.UnitId
                          join f in factories on u.FactoryId equals f.ID
                          select new { Unit = u, Tank = t, Factory = f }).ToList();
            foreach (var r in result)
            {
                Console.WriteLine($"Название резервуара = {r.Tank.Name}, название цеха = {r.Unit.Name}, название фабрики = {r.Factory.Name}\n");
            }

            var serialize = Serializer.Serialize(factories, units, tanks);
            File.WriteAllText("struct.json", serialize);

            double sum = 0;
            foreach (var t in tanks)
            {
                sum += t.Volume;
            }

            Console.WriteLine($"Сумма загрузки всех резервуаров: {sum}");
                    

            Console.WriteLine("В какой коллекции выполнить поиск?\n");
            string searhName;
            string typeName = Console.ReadLine();
            switch (typeName)
            {
                case "фабрика":
                    Console.WriteLine("Введите название фабрики\n");
                    searhName = Console.ReadLine().ToLower();
                    var searchFactory = factories.Where(f => f.Name.ToLower().Contains(searhName) || f.Desc.ToLower().Contains(searhName));
                    foreach (var s in searchFactory)
                    {
                        Console.WriteLine($"Название фабрики: {s.Name}, Описание: {s.Desc}");
                    }
                    break;
                case "установка":
                    Console.WriteLine("Введите название установки\n");
                    searhName = Console.ReadLine();
                    var searchUnit = units.Where(u => u.Name.ToLower().Contains(searhName));
                    foreach (var s in searchUnit)
                    {
                        Console.WriteLine($"Название установки: {s.Name}, Номер фабрики: {s.FactoryId}");
                    }
                    break;
                case "резервуар":
                    Console.WriteLine("Введите название или параметр резервуара\n");
                    searhName = Console.ReadLine();
                    var searchTank = tanks.Where(t => t.Name.ToLower().Contains(searhName) || t.Volume.ToString() == searhName || t.MaxVolume.ToString() == searhName); ;
                    foreach (var s in searchTank)
                    {
                        Console.WriteLine($"Название резервуара: {s.Name}, заполнение: {s.Volume}, максимальный объем {s.MaxVolume}");
                    }
                    break;
            }

        }
    }
}