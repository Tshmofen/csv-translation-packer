using CsvTranslationPacker.Models;
using CsvTranslationPacker.Services;
using System.Text.Json;
using Validator = CsvTranslationPacker.Validation.Validator;

try
{
    Reporter.Report("Starting parser!");

    if (!SettingsManager.IsSettingsExists())
    {
        SettingsManager.GenerateSettings();
        Reporter.Report("Default settings file was created, run app again to use it.", ConsoleColor.DarkGreen);
        Reporter.WaitForInput();
        return;
    }

    var settings = SettingsManager.Settings;
    var zipFile = ZipReader.GetZipFile(settings.InputFileSearch);
    var files = ZipReader.GetFilesFromZip(zipFile);
    var fileNames = files.ConvertAll(f => f.fileName);

    if (!Validator.ValidateTranslationFiles(fileNames))
    {
        throw new ApplicationException("All files in the directory should end with '.{lang}.csv.'");
    }

    Reporter.Report($"Zip content naming is validated for {fileNames.Count} file(s).");

    var translations = new List<Translation>();
    foreach (var (fileName, content) in files)
    {
        var language = TranslationManager.GetTranslationLanguageFromFileName(fileName);
        var translationList = TranslationManager.ParseTranslations(content);

        foreach(var translation in translationList)
        {
            translation.Language = language;
        }

        translations.AddRange(translationList);
    }

    Reporter.Report($"Separate {translations.Count} translations in different {fileNames.Count} language(s) are successfully parsed.");
    Reporter.Report($"Counted {translations.Select(t => t.Key).Distinct().Count()} distinct translation keys.");

    TranslationManager.WriteTranslationsInOneFile(translations);

    Reporter.Report($"File '{settings.OutputFile}' was successfully created!", ConsoleColor.DarkGreen, true);
}
catch (Exception e)
{
    if (e is not IOException and not ApplicationException and not JsonException)
    {
        #if !DEBUG
        e = new ApplicationException("Sorry. Unexpected error.");
        #endif
    }

    Reporter.Report($"Parser error. {e.Message}", ConsoleColor.DarkRed, true);
}

Reporter.WaitForInput();
