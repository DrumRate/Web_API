using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;


namespace FirstTask
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\\Users\\dnevn\\Downloads\\Таблица_резервуаров.xlsx";
            var excel = new Excel();

            var factories = ExcelConverter.ConvertToFactory(excel.readExcel(path, 0));
            var units = ExcelConverter.ConvertToUnit(excel.readExcel(path, 1));
            var tanks = ExcelConverter.ConvertToTank(excel.readExcel(path, 2));

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

            Console.WriteLine("В какой коллекции выполнить поиск?\n");
            string searhName;
            string typeName = Console.ReadLine();
            switch (typeName)
            {
                case "1":
                    Console.WriteLine("Введите название фабрики\n");
                    searhName = Console.ReadLine();
                    var searchFactory = factories.Where(f => f.Name.Contains(searhName) || f.Desc.Contains(searhName));
                    foreach (var s in searchFactory)
                    {
                        Console.WriteLine($"Название фабрики: {s.Name}, Описание: {s.Desc}");
                    }
                    break;
                case "2":
                    Console.WriteLine("Введите название установки\n");
                    searhName = Console.ReadLine();
                    var searchUnit = units.Where(u => u.Name.Contains(searhName));
                    foreach (var s in searchUnit)
                    {
                        Console.WriteLine($"Название установки: {s.Name}, Номер фабрики: {s.FactoryId}");
                    }
                    break;
                case "3":
                    Console.WriteLine("Введите название или параметр резервуара\n");
                    searhName = Console.ReadLine();
                    var searchTank = tanks.Where(t => t.Name.Contains(searhName) || t.Volume.ToString() == searhName || t.MaxVolume.ToString() == searhName); ;
                    foreach (var s in searchTank)
                    {
                        Console.WriteLine($"Название резервуара: {s.Name}, заполнение: {s.Volume}, максимальный объем {s.MaxVolume}");
                    }
                    break;
            }



        }
    }
}