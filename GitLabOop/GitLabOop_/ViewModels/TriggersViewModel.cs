using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class TriggersViewModel : LocalizedViewModelBase
{
    public TriggersViewModel(ILocalizationService localization)
        : base(localization)
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool isOnline = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool isPriorityMode;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentState))]
    private bool hasWarnings;

    public string CurrentState =>
        string.Format(
            T("Triggers_CurrentState"),
            T(IsOnline ? "Common_Yes" : "Common_No"),
            T(IsPriorityMode ? "Common_Yes" : "Common_No"),
            T(HasWarnings ? "Common_Yes" : "Common_No"));

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

    protected override void OnLanguageChanged()
    {
        OnPropertyChanged(nameof(CurrentState));
    }
}
