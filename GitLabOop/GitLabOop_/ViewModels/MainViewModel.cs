using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Models;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public class MainViewModel : ObservableObject
{
    private LanguageOption selectedLanguage;

    public MainViewModel()
    {
        var currentLanguageCode = LocalizationManager.Instance.CurrentCulture.TwoLetterISOLanguageName;
        selectedLanguage = Languages.First(language => language.Code == currentLanguageCode);
    }

    public DefaultBindingViewModel DefaultBinding { get; } = new();

    public TwoWayBindingViewModel TwoWayBinding { get; } = new();

    public OneTimeBindingViewModel OneTimeBinding { get; } = new();

    public OneWayBindingsViewModel OneWayBindings { get; } = new();

    public TriggersViewModel Triggers { get; } = new();

    public ObservableCollection<LanguageOption> Languages { get; } =
    [
        new LanguageOption("ru", "LanguageOptionRussian"),
        new LanguageOption("en", "LanguageOptionEnglish")
    ];

    public LanguageOption SelectedLanguage
    {
        get => selectedLanguage;
        set
        {
            if (SetProperty(ref selectedLanguage, value))
            {
                LocalizationManager.Instance.SetCulture(value.Code);
            }
        }
    }
}
