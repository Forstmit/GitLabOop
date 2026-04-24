using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    private string notesToSource = "Начальное значение модели";

    public string SystemMessage => TelemetryLevel switch
    {
        < 35 => "Низкая загрузка. Интерфейс работает в спокойном режиме.",
        < 70 => "Средняя загрузка. Значения приходят только из модели.",
        _ => "Высокая загрузка. UI обновляется от источника, но не пишет обратно."
    };

    public string LevelCaption => $"Текущее значение потока: {TelemetryLevel:F0}%";

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
}
