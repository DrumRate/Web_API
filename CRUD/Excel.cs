using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace CRUD
{
    class Excel : IExcelReader
    {
        public IEnumerable<IEnumerable<string>> readExcel(string path, int sheetnum)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                try
                {
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets[sheetnum];
                    var ThatList = worksheet.Cells;
                    var rowCnt = worksheet.Dimension.End.Row;
                    var colCnt = worksheet.Dimension.End.Column;
                    var result = new List<IEnumerable<string>>(ThatList.Rows);
                    //loop all rows
                    for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var data = new List<string>(worksheet.Dimension.End.Column);
                        //loop all columns in a row
                        for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                        {
                            //add the cell data to the List
                            if (worksheet.Cells[i, j].Value != null)
                            {
                                data.Add(worksheet.Cells[i, j].Value.ToString());
                            }
                        }
                        result.Add(data);
                    }
                    return result;
                }
                catch (Exception e)
                {

                    throw new IndexOutOfRangeException("Нет файла", e);

                }
            }
        }
    }
}