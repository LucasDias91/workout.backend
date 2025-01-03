using OfficeOpenXml;
namespace Workout.Infra.CrossCutting.Excel
{
    public static class Excel
    {
        public static List<T> ToList<T>(string location, string workSheet)
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(location)))
                {
                    var sheet = package.Workbook.Worksheets[workSheet];
                    List<T> list = new List<T>();
                    var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToList().Select(n =>

                        new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() }
                    );

                    for (int row = 2; row <= sheet.Dimension.Rows; row++)
                    {
                        if (row == 0 || row == 1)
                            continue;

                        T obj = (T)Activator.CreateInstance(typeof(T));//generic object
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            int col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                            var val = sheet.Cells[row, col].Value;
                            var propType = prop.PropertyType;
                            prop.SetValue(obj, Convert.ChangeType(val, propType));
                        }
                        list.Add(obj);
                    }

                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
