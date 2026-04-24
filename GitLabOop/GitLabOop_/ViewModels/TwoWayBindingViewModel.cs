using CommunityToolkit.Mvvm.ComponentModel;

namespace GitLabOop.ViewModels;

public partial class TwoWayBindingViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string fullName = "Иван Иванов";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string feedback = "Запрос подтверждён";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private double budget = 35000;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private bool pushEnabled = true;

    public string LiveSummary => $"{FullName} | Бюджет: {Budget:F0} ₽ | Push: {(PushEnabled ? "включены" : "выключены")} | {Feedback}";
}
