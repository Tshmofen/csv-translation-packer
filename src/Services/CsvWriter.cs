using System.Data;

namespace CsvTranslationPacker.Services;

public static class CsvWriter
{
    public static void SaveDataTableAsCsv(DataTable dataTable, string filePath, string delimiter = ",")
    {
        using var writer = new StreamWriter(filePath, false);

        for (var i = 0; i < dataTable.Columns.Count; i++)
        {
            writer.Write(dataTable.Columns[i]);

            if (i < dataTable.Columns.Count - 1)
            {
                writer.Write(delimiter);
            }
        }

        var count = 0;
        writer.Write(writer.NewLine);
        Reporter.Report($"Writing: {count++}/{dataTable.Rows.Count} rows.");

        foreach (DataRow row in dataTable.Rows)
        {
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                var value = row[i].ToString()!;

                if (value.Contains(delimiter))
                {
                    value = $"\"{value}\"";
                    writer.Write(value);
                }
                else
                {
                    writer.Write(row[i].ToString());
                }

                if (i < dataTable.Columns.Count - 1)
                {
                    writer.Write(delimiter);
                }
            }

            Reporter.ReportReplaceLine($"Writing: {count++}/{dataTable.Rows.Count} rows.");
            writer.Write(writer.NewLine);
        }
    }
}
