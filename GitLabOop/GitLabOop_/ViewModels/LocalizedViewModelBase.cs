using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public abstract class LocalizedViewModelBase : ObservableObject
{
    protected LocalizedViewModelBase(ILocalizationService localization)
    {
        Localization = localization;
        Localization.LanguageChanged += (_, _) => OnLanguageChanged();
    }

    protected ILocalizationService Localization { get; }

    protected string T(string key) => Localization[key];

    protected virtual void OnLanguageChanged()
    {
    }
}
