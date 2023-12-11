using System.Data;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvTranslationPacker.Models;

namespace CsvTranslationPacker.Services;

public static class TranslationManager
{
    private static List<string> GetTranslationsLanguages(IEnumerable<Translation> translations)
    {
        return translations
            .Select(t => t.Language)
            .Distinct()
            .OrderBy(t => t)
            .ToList()!;
    }

    private static List<string> GetTranslationsKeys(IEnumerable<Translation> translations)
    {
        return translations
            .Select(t => t.Key)
            .Distinct()
            .ToList()!;
    }

    public static object?[] GetTranslatedValues(string key, List<Translation> translations, List<string> languages)
    {
        var output = new object?[languages.Count + 1];

        var index = 0;
        output[index] = key;
        foreach (var language in languages)
        {
            index++;
            output[index] = translations.Find(t => t.Key == key && t.Language == language)?.Value ?? "[NO TRANSLATION FOUND]";
        }

        return output;
    }

    public static void WriteTranslationsInOneFile(List<Translation> translations)
    {
        var settings = SettingsManager.Settings;
        var languages = GetTranslationsLanguages(translations);
        var keys = GetTranslationsKeys(translations);

        using var table = new DataTable();

        table.Columns.Add(settings.KeyColumnName);
        foreach (var language in languages)
        {
            table.Columns.Add(language);
        }

        foreach (var key in keys)
        {
            table.Rows.Add(GetTranslatedValues(key, translations, languages));
        }

        table.AcceptChanges();
        CsvWriteHelper.SaveDataTableAsCsv(table, settings.OutputFile, settings.Delimiter);
    }

    public static IList<Translation> ParseTranslations(byte[] csvContent)
    {
        var settings = SettingsManager.Settings;
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = settings.InputHasHeaders,
            Delimiter = settings.Delimiter
        };

        using var fileReader = new StringReader(Encoding.UTF8.GetString(csvContent));
        using var csvParser = new CsvReader(fileReader, config);

        csvParser.Context.RegisterClassMap(new TranslationMap(settings.KeyColumnIndex, settings.ValueColumnIndex));
        return csvParser.GetRecords<Translation>().ToList();
    }

    public static string GetTranslationLanguageFromFileName(string fileWithoutCsv)
    {
        fileWithoutCsv = fileWithoutCsv.Replace(".csv", "");
        // file expected to have '.{lang}.csv' ending
        return Path.GetExtension(fileWithoutCsv).Replace(".", string.Empty);
    }
}
