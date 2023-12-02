using CsvTranslationPacker;

var initialColor = Console.ForegroundColor;

try
{
    if (args.Length == 0 || !args[0].EndsWith(".zip"))
    {
       throw new ApplicationException("Provide path to zip with translations");
    }

    var zipPath = args[0];
    var files = ZipReader.GetFilesFromZip(zipPath);
    var fileNames = files.ConvertAll(f => f.fileName);

    if (!Validator.ValidateTranslationFiles(fileNames))
    {
        throw new ApplicationException("All files in the directory should end with '.{lang}.csv'");
    }

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

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    TranslationManager.WriteTranslationsInOneFile(translations, "./translations.csv");
    Console.WriteLine("File 'translations.csv' successfully created");
}
catch (Exception e)
{
    if (e is not IOException and not ApplicationException)
    {
        #if !DEBUG
        e = new ApplicationException("Sorry. Unexpected error.");
        #endif
    }

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine(e.Message);
}
finally
{
    Console.ForegroundColor = initialColor;
}

Console.Write("Press any key to continue...");
Console.ReadKey();
