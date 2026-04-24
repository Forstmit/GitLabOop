using System.ComponentModel;
using GitLabOop.Models;

namespace GitLabOop.Services;

public interface ILocalizationService : INotifyPropertyChanged
{
    IReadOnlyList<LanguageOption> Languages { get; }

    string CurrentLanguage { get; }

    string this[string key] { get; }

    event EventHandler? LanguageChanged;

    void SetLanguage(string languageCode);
}
