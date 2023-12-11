using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using CsvHelper;

namespace CsvTranslationPacker.Services;

public static class CsvWriteHelper
{
    public static void SaveDataTableAsCsv(DataTable dataTable, string filePath, string delimiter = ",")
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = delimiter
        };

        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, config);

        foreach (DataColumn column in dataTable.Columns)
        {
            csv.WriteField(column.ColumnName);
        }
        csv.NextRecord();

        var count = 0;
        Reporter.Report($"Writing: {count++}/{dataTable.Rows.Count} rows.");

        foreach (DataRow row in dataTable.Rows)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                csv.WriteField(row[column]);
            }

            csv.NextRecord();
            Reporter.ReportReplaceLine($"Writing: {count++}/{dataTable.Rows.Count} rows.");
        }

        dataTable.Dispose();
    }
}
