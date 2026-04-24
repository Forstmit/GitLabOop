using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Models;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class MainViewModel : LocalizedViewModelBase
{
    public MainViewModel(ILocalizationService localization)
        : base(localization)
    {
        SupportedLanguages = localization.Languages;
        DefaultBinding = new DefaultBindingViewModel(localization);
        TwoWayBinding = new TwoWayBindingViewModel(localization);
        OneTimeBinding = new OneTimeBindingViewModel(localization);
        OneWayBindings = new OneWayBindingsViewModel(localization);
        Triggers = new TriggersViewModel(localization);
        SelectedLanguage = SupportedLanguages.First(language => language.Code == localization.CurrentLanguage);
    }

    public IReadOnlyList<LanguageOption> SupportedLanguages { get; }

    public DefaultBindingViewModel DefaultBinding { get; }

    public TwoWayBindingViewModel TwoWayBinding { get; }

    public OneTimeBindingViewModel OneTimeBinding { get; }

    public OneWayBindingsViewModel OneWayBindings { get; }

    public TriggersViewModel Triggers { get; }

    [ObservableProperty]
    private LanguageOption? selectedLanguage;

    public string CurrentLanguageCaption =>
        string.Format(T("MainWindow_CurrentLanguage"), SelectedLanguage?.DisplayName ?? Localization.CurrentLanguage);

    partial void OnSelectedLanguageChanged(LanguageOption? value)
    {
        if (value is null || value.Code == Localization.CurrentLanguage)
        {
            return;
        }

        Localization.SetLanguage(value.Code);
    }

    [RelayCommand]
    private void ShowLocalizedMessage()
    {
        var message = string.Format(T("MessageBox_Body"), SelectedLanguage?.DisplayName ?? Localization.CurrentLanguage);
        MessageBox.Show(message, T("MessageBox_Title"), MessageBoxButton.OK, MessageBoxImage.Information);
    }

    protected override void OnLanguageChanged()
    {
        OnPropertyChanged(nameof(CurrentLanguageCaption));
        var current = SupportedLanguages.First(language => language.Code == Localization.CurrentLanguage);
        if (SelectedLanguage?.Code != current.Code)
        {
            SelectedLanguage = current;
        }
    }
}
