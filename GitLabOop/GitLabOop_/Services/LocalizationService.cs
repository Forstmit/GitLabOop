using System.IO;
using CodingSeb.Localization;
using CodingSeb.Localization.Loaders;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Models;

namespace GitLabOop.Services;

public partial class LocalizationService : ObservableObject, ILocalizationService
{
    private static readonly object SyncRoot = new();
    private static bool isInitialized;
    private static IReadOnlyList<LanguageOption>? cachedLanguages;

    public LocalizationService()
    {
        EnsureInitialized();
        Languages = cachedLanguages ?? Array.Empty<LanguageOption>();
    }

    public IReadOnlyList<LanguageOption> Languages { get; }

    public string CurrentLanguage => Loc.Instance.CurrentLanguage;

    public string this[string key] => Loc.Tr(key, $"!{key}!");

    public event EventHandler? LanguageChanged;

    public void SetLanguage(string languageCode)
    {
        if (CurrentLanguage == languageCode)
        {
            return;
        }

        Loc.Instance.CurrentLanguage = languageCode;
        OnPropertyChanged(nameof(CurrentLanguage));
        OnPropertyChanged("Item[]");
        OnPropertyChanged(string.Empty);
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    private static void EnsureInitialized()
    {
        if (isInitialized)
        {
            return;
        }

        lock (SyncRoot)
        {
            if (isInitialized)
            {
                return;
            }

            LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
            LocalizationLoader.Instance.AddFile(ResolveLocalizationFilePath());
            Loc.Instance.CurrentLanguage = "ru";

            cachedLanguages = new[]
            {
                new LanguageOption { Code = "ru", DisplayName = "Русский" },
                new LanguageOption { Code = "en", DisplayName = "English" }
            };

            isInitialized = true;
        }
    }

    private static string ResolveLocalizationFilePath()
    {
        var outputPath = Path.Combine(AppContext.BaseDirectory, "Localization", "Strings.loc.json");
        if (File.Exists(outputPath))
        {
            return outputPath;
        }

        var projectPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "Localization",
            "Strings.loc.json"));

        if (File.Exists(projectPath))
        {
            return projectPath;
        }

        throw new DirectoryNotFoundException(
            $"Could not find localization file. Checked '{outputPath}' and '{projectPath}'.");
    }
}
