using System;
using System.Globalization;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GitLabOop.Services;

public sealed class LocalizationManager : ObservableObject
{
    private readonly ResourceManager resourceManager = new("GitLabOop.Properties.Strings", typeof(LocalizationManager).Assembly);
    private CultureInfo currentCulture = CultureInfo.GetCultureInfo("ru");

    private LocalizationManager()
    {
        ApplyCulture(currentCulture);
    }

    public static LocalizationManager Instance { get; } = new();

    public event EventHandler? LanguageChanged;

    public CultureInfo CurrentCulture => currentCulture;

    public string this[string key] => resourceManager.GetString(key, currentCulture) ?? $"!{key}!";

    public string GetString(string key) => this[key];

    public string Format(string key, params object[] arguments)
    {
        return string.Format(currentCulture, GetString(key), arguments);
    }

    public void SetCulture(string cultureCode)
    {
        var culture = CultureInfo.GetCultureInfo(cultureCode);
        if (string.Equals(currentCulture.Name, culture.Name, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        currentCulture = culture;
        ApplyCulture(culture);
        OnPropertyChanged(nameof(CurrentCulture));
        OnPropertyChanged("Item[]");
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    private static void ApplyCulture(CultureInfo culture)
    {
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
