using System.Collections.Generic;

namespace CRUD
{
    internal interface IExcelReader
    {
        interface IExcelReader
        {
            IEnumerable<IEnumerable<string>> readExcel(string path, int sheetnum);
        }
    }
}