using CsvHelper.Configuration;

namespace CsvTranslationPacker.Models;

public class Translation
{
    public string? Language { get; set; }
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public sealed class TranslationMap : ClassMap<Translation>
{
    public TranslationMap(int keyIndex, int valueIndex)
    {
        Map(x => x.Key).Index(keyIndex);
        Map(x => x.Value).Index(valueIndex);
    }
}
