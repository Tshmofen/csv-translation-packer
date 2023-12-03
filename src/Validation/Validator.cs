using System.Text.RegularExpressions;

namespace CsvTranslationPacker.Validation;

public static partial class Validator
{
    [GeneratedRegex(@"[^.]*\.[^.]*\.csv")]
    private static partial Regex TranslationCsvRegex();

    public static bool ValidateTranslationFiles(IList<string> fileNames)
    {
        var regex = TranslationCsvRegex();
        return fileNames.Count != 0 && fileNames.Select(Path.GetFileName).All(regex.IsMatch!);
    }
}
