using System.Data;

namespace CsvTranslationPacker;

public static class CsvWriter
{
    public static void SaveDataTableAsCsv(DataTable dtDataTable, string filePath, string delimiter = ",")
    {
        using var sw = new StreamWriter(filePath, false);

        for (var i = 0; i < dtDataTable.Columns.Count; i++)
        {
            sw.Write(dtDataTable.Columns[i]);

            if (i < dtDataTable.Columns.Count - 1)
            {
                sw.Write(delimiter);
            }
        }

        sw.Write(sw.NewLine);

        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (var i = 0; i < dtDataTable.Columns.Count; i++)
            {
                var value = dr[i].ToString()!;

                if (value.Contains(delimiter))
                {
                    value = $"\"{value}\"";
                    sw.Write(value);
                }
                else
                {
                    sw.Write(dr[i].ToString());
                }

                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(delimiter);
                }
            }

            sw.Write(sw.NewLine);
        }
    }
}
