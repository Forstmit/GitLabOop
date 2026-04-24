using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Models;

namespace GitLabOop.Services;

public partial class LocalizationService : ObservableObject, ILocalizationService
{
    private readonly Dictionary<string, Uri> dictionaryMap = new()
    {
        ["ru"] = new("/GitLabOop;component/Resources/Localization/Strings.ru.xaml", UriKind.Relative),
        ["en"] = new("/GitLabOop;component/Resources/Localization/Strings.en.xaml", UriKind.Relative)
    };

    private string currentLanguage = "ru";
    private ResourceDictionary currentDictionary;

    public LocalizationService()
    {
        Languages = new[]
        {
            new LanguageOption { Code = "ru", DisplayName = "Русский" },
            new LanguageOption { Code = "en", DisplayName = "English" }
        };

        currentDictionary = LoadDictionary(currentLanguage);
    }

    public IReadOnlyList<LanguageOption> Languages { get; }

    public string CurrentLanguage => currentLanguage;

    public string this[string key] => currentDictionary[key] as string ?? $"!{key}!";

    public event EventHandler? LanguageChanged;

    public void SetLanguage(string languageCode)
    {
        if (currentLanguage == languageCode)
        {
            return;
        }

        currentDictionary = LoadDictionary(languageCode);
        currentLanguage = languageCode;

        OnPropertyChanged(nameof(CurrentLanguage));
        OnPropertyChanged("Item[]");
        OnPropertyChanged(string.Empty);
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    private ResourceDictionary LoadDictionary(string languageCode)
    {
        return new ResourceDictionary { Source = dictionaryMap[languageCode] };
    }
}
