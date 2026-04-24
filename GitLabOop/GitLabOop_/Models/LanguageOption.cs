using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.Models;

public partial class LanguageOption : ObservableObject
{
    private readonly string displayNameKey;

    public LanguageOption(string code, string displayNameKey)
    {
        Code = code;
        this.displayNameKey = displayNameKey;
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    public string Code { get; }

    public string DisplayName => LocalizationManager.Instance[displayNameKey];

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(DisplayName));
    }
}
