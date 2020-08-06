using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject
{
    interface IExcelReader
    {
        IEnumerable<IEnumerable<string>> readExcel(string path, int sheetnum);
    }
}
