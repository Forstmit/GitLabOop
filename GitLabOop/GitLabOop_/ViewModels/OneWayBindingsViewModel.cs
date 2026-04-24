using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class OneWayBindingsViewModel : LocalizedViewModelBase
{
    public OneWayBindingsViewModel(ILocalizationService localization)
        : base(localization)
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SystemMessage))]
    [NotifyPropertyChangedFor(nameof(LevelCaption))]
    private double telemetryLevel = 41;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SystemMessage))]
    private bool hasSync = true;

    [ObservableProperty]
    private string notesToSource = "Начальное значение модели";

    public string SystemMessage => TelemetryLevel switch
    {
        < 35 => T("OneWay_SystemMessageLow"),
        < 70 => T("OneWay_SystemMessageMedium"),
        _ => T("OneWay_SystemMessageHigh")
    };

    public string LevelCaption => string.Format(T("OneWay_LevelCaption"), TelemetryLevel);

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

    protected override void OnLanguageChanged()
    {
        OnPropertyChanged(nameof(SystemMessage));
        OnPropertyChanged(nameof(LevelCaption));
    }
}
