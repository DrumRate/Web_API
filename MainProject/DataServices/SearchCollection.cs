using MainProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainProject
{
    class SearchCollection
    {
        public void SearchInCollection(DataManager dataManager)
        {
            var console = new ConsoleFacade();
            console.onConsoleReading += (message) => { console.WriteInConsole($"Вы ввели {message} в {DateTime.Now}"); };
            Console.WriteLine("В какой коллекции выполнить поиск?\n");
            string searchName;
            var key = Console.ReadKey();
            var flag = true;
            while (flag)
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    try
                    {
                        Console.WriteLine("\nВведите название фабрики\n");
                        searchName = console.ReadLineConsole().ToLower();
                        
                        var searchFactory = dataManager.Factories.Where(f => f.Name.ToLower().Contains(searchName) || f.Desc.ToLower().Contains(searchName));
                        if (!searchFactory.Any()) throw new InvalidOperationException();
                        foreach (var s in searchFactory)
                        {
                            Console.WriteLine($"Название фабрики: {s.Name}, Описание: {s.Desc}");
                        }
                        flag = false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("\nФабрика не найдена");
                    }
                    
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    try
                    {
                        Console.WriteLine("\nВведите название установки\n");
                        searchName = console.ReadLineConsole().ToLower();
                        //var searchUnit = excel.units.Where(u => u.Name.ToLower().Contains(searhName));
                        var searchUnit = (from u in dataManager.Units
                                          where u.Name.ToLower().Contains(searchName)
                                          select u);
                        if (!searchUnit.Any()) throw new InvalidOperationException();
                        foreach (var s in searchUnit)
                        {
                            Console.WriteLine($"Название установки: {s.Name}, Номер фабрики: {s.FactoryId}");
                        }
                        flag = false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("\nУстановка не найдена");
                    }
                   
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    try
                    {
                        Console.WriteLine("\nВведите название или параметр резервуара\n");
                        searchName = console.ReadLineConsole().ToLower();
                        var searchTank = dataManager.Tanks.Where(t => t.Name.ToLower().Contains(searchName) || t.Volume.ToString() == searchName || t.MaxVolume.ToString() == searchName);
                        if (!searchTank.Any()) throw new InvalidOperationException();
                        foreach (var s in searchTank)
                        {
                            Console.WriteLine($"\nНазвание резервуара: {s.Name}, заполнение: {s.Volume}, максимальный объем {s.MaxVolume}");
                        }
                        flag = false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Резервуар не найден!");
                    }
                    
                }
                else
                {
                    flag = false;
                }
        }
    }
}
