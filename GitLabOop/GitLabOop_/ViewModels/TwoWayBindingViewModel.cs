using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class TwoWayBindingViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string fullName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string feedback = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private double budget = 35000;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private bool pushEnabled = true;

    private string lastLocalizedFullName = string.Empty;
    private string lastLocalizedFeedback = string.Empty;

    public TwoWayBindingViewModel()
    {
        ApplyLocalizedSeeds(forceUpdate: true);
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    public string LiveSummary => LocalizationManager.Instance.Format(
        "TwoWayLiveSummaryFormat",
        FullName,
        Budget,
        PushEnabled
            ? LocalizationManager.Instance["TwoWayPushEnabledValue"]
            : LocalizationManager.Instance["TwoWayPushDisabledValue"],
        Feedback);

    private void ApplyLocalizedSeeds(bool forceUpdate = false)
    {
        var localizer = LocalizationManager.Instance;
        var localizedFullName = localizer.GetString("TwoWayFullNameInitial");
        var localizedFeedback = localizer.GetString("TwoWayFeedbackInitial");

        if (forceUpdate || string.IsNullOrWhiteSpace(FullName) || FullName == lastLocalizedFullName)
        {
            FullName = localizedFullName;
        }

        if (forceUpdate || string.IsNullOrWhiteSpace(Feedback) || Feedback == lastLocalizedFeedback)
        {
            Feedback = localizedFeedback;
        }

        lastLocalizedFullName = localizedFullName;
        lastLocalizedFeedback = localizedFeedback;
    }

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        ApplyLocalizedSeeds();
        OnPropertyChanged(nameof(LiveSummary));
    }
}
