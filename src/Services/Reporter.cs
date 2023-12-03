namespace CsvTranslationPacker.Services;

public static class Reporter
{
    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    private static void WriteLineColor(string text, ConsoleColor? color = null)
    {
        var initialColor = Console.ForegroundColor;
        Console.ForegroundColor = color ?? initialColor;
        Console.WriteLine(text);
        Console.ForegroundColor = initialColor;
    }

    public static void Report(string text, ConsoleColor? color = null, bool required = false)
    {
        if (!required && !SettingsManager.Settings.ShowProgress)
        {
            return;
        }

        WriteLineColor(text, color);
    }

    public static void ReportReplaceLine(string text, ConsoleColor? color = null, bool required = false)
    {
        if (!required && !SettingsManager.Settings.ShowProgress)
        {
            return;
        }

        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        WriteLineColor(text, color);
    }

    public static void WaitForInput()
    {
        Report("Press any key to continue...",  required: true);
        Console.ReadKey();
    }
}
