namespace CsvTranslationPacker.Models;

public class Settings
{
    public string InputFileSearch { get; set; } = "*.zip";
    public string OutputFile { get; set; } = "translation.csv";
    public int KeyColumnIndex { get; set; } = 0;
    public int ValueColumnIndex { get; set; } = 2;
    public string Delimiter { get; set; } = ",";
    public bool ShowProgress { get; set; } = true;
    public bool InputHasHeaders { get; set; } = false;
    public string KeyColumnName { get; set; } = "Key";
}