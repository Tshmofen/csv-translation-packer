using CsvTranslationPacker.Models;
using Newtonsoft.Json;

namespace CsvTranslationPacker.Services;

public static class SettingsManager
{
    private const string SettingsPath = "./settings.json";
    private static Settings? _settings;

    public static bool IsSettingsExists()
    {
        return File.Exists(SettingsPath);
    }

    public static void GenerateSettings()
    {
        _settings = new Settings();
        var text = JsonConvert.SerializeObject(_settings, Formatting.Indented);
        File.WriteAllText(SettingsPath, text);
    }

    public static Settings Settings
    {
        get 
        {
            if (_settings != null)
            {
                return _settings;
            }

            if (!IsSettingsExists())
            {
                return new Settings();
            }

            var file = File.ReadAllText(SettingsPath);
            return _settings = JsonConvert.DeserializeObject<Settings>(file) ?? new Settings();
        }
    }
}