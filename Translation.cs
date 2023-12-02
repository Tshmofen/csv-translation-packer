using CsvHelper.Configuration.Attributes;

namespace CsvTranslationPacker;

public class Translation
{
    public string? Language { get; set; }

    [Index(0)]
    public string? Key {  get; set; }

    [Index(2)]
    public string? Value { get; set; }
}
