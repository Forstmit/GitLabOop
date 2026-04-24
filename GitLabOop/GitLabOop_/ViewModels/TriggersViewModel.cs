using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GitLabOop.ViewModels;

public partial class TriggersViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool isOnline = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool isPriorityMode;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool hasWarnings;

    public string CurrentState => $"Online: {(IsOnline ? "Да" : "Нет")} | Priority: {(IsPriorityMode ? "Да" : "Нет")} | Warnings: {(HasWarnings ? "Да" : "Нет")}";

    [RelayCommand]
    private void ToggleOnline()
    {
        IsOnline = !IsOnline;
    }

    [RelayCommand]
    private void TogglePriority()
    {
        IsPriorityMode = !IsPriorityMode;
    }

    [RelayCommand]
    private void ToggleWarnings()
    {
        HasWarnings = !HasWarnings;
    }

    [RelayCommand]
    private void Reset()
    {
        IsOnline = true;
        IsPriorityMode = false;
        HasWarnings = false;
    }
}
