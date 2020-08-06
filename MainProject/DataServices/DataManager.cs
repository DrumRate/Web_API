using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MainProject
{
    class DataManager
    {
        public IEnumerable<Factory> Factories;
        public IEnumerable<Unit> Units;
        public IEnumerable<Tank> Tanks;
        public Task GetFactoriesFromExcel()
        {
            
            try
            {
                var dirInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent?.Parent?.Parent;
                string path = Path.Combine(dirInfo.FullName, "Таблица_резервуаров.xlsx");
                return new Task(() =>
                {
                    var excel = new Excel();
                    Factories = ExcelConverter.ConvertToFactory(excel.readExcel(path, 0)).ToList();
                    Units = ExcelConverter.ConvertToUnit(excel.readExcel(path, 1)).ToList();
                    Tanks = ExcelConverter.ConvertToTank(excel.readExcel(path, 2)).ToList();
                });

                //return Task.Factory.StartNew()
                //    .ContinueWith(r => 
                //{
                //    if (r.IsFaulted)
                //    {
                //        throw r.Exception;
                //    }
                //        })
                ;
                
            }
            catch (Exception e)
            {

                throw new FileNotFoundException("Файл не найден!");
            }
        }
    }
}
