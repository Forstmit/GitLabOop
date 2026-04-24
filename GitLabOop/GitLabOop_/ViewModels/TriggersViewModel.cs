using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Services;

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

    public TriggersViewModel()
    {
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    public string CurrentState => LocalizationManager.Instance.Format(
        "TriggersCurrentStateFormat",
        LocalizationManager.Instance["TriggersOnlineLabel"],
        IsOnline ? LocalizationManager.Instance["CommonYes"] : LocalizationManager.Instance["CommonNo"],
        LocalizationManager.Instance["TriggersPriorityLabel"],
        IsPriorityMode ? LocalizationManager.Instance["CommonYes"] : LocalizationManager.Instance["CommonNo"],
        LocalizationManager.Instance["TriggersWarningsLabel"],
        HasWarnings ? LocalizationManager.Instance["CommonYes"] : LocalizationManager.Instance["CommonNo"]);

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

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentState));
    }
}
