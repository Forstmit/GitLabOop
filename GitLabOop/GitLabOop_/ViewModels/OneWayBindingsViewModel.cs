using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class OneWayBindingsViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SystemMessage))]
    [NotifyPropertyChangedFor(nameof(LevelCaption))]
    private double telemetryLevel = 41;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SystemMessage))]
    private bool hasSync = true;

    [ObservableProperty]
    private string notesToSource = string.Empty;

    private string lastLocalizedNotesToSource = string.Empty;

    public OneWayBindingsViewModel()
    {
        ApplyLocalizedSeeds(forceUpdate: true);
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    public string SystemMessage => TelemetryLevel switch
    {
        < 35 => LocalizationManager.Instance["TelemetryLowLoadMessage"],
        < 70 => LocalizationManager.Instance["TelemetryMediumLoadMessage"],
        _ => LocalizationManager.Instance["TelemetryHighLoadMessage"]
    };

    public string LevelCaption => LocalizationManager.Instance.Format("TelemetryLevelCaptionFormat", TelemetryLevel);

    [RelayCommand]
    private void AdvanceTelemetry()
    {
        TelemetryLevel += 18;
        if (TelemetryLevel > 100)
        {
            TelemetryLevel = 16;
            HasSync = !HasSync;
        }
    }

    private void ApplyLocalizedSeeds(bool forceUpdate = false)
    {
        var localizedNotes = LocalizationManager.Instance.GetString("OneWayNotesToSourceInitial");
        if (forceUpdate || string.IsNullOrWhiteSpace(NotesToSource) || NotesToSource == lastLocalizedNotesToSource)
        {
            NotesToSource = localizedNotes;
        }

        lastLocalizedNotesToSource = localizedNotes;
    }

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        ApplyLocalizedSeeds();
        OnPropertyChanged(nameof(SystemMessage));
        OnPropertyChanged(nameof(LevelCaption));
    }
}
