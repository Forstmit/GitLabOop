using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class DefaultBindingViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private string presenterName = string.Empty;

    [ObservableProperty]
    private string notes = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccentOpacityRatio))]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private double accentOpacity = 68;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private bool isPinned = true;

    private string lastLocalizedPresenterName = string.Empty;
    private string lastLocalizedNotes = string.Empty;

    public DefaultBindingViewModel()
    {
        ApplyLocalizedSeeds(forceUpdate: true);
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    public double AccentOpacityRatio => AccentOpacity / 100.0;

    public string Summary => LocalizationManager.Instance.Format(
        "DefaultBindingSummaryFormat",
        PresenterName,
        AccentOpacity,
        IsPinned ? LocalizationManager.Instance["CommonYes"] : LocalizationManager.Instance["CommonNo"]);

    private void ApplyLocalizedSeeds(bool forceUpdate = false)
    {
        var localizer = LocalizationManager.Instance;
        var localizedPresenterName = localizer.GetString("DefaultBindingPresenterNameInitial");
        var localizedNotes = localizer.GetString("DefaultBindingNotesInitial");

        if (forceUpdate || string.IsNullOrWhiteSpace(PresenterName) || PresenterName == lastLocalizedPresenterName)
        {
            PresenterName = localizedPresenterName;
        }

        if (forceUpdate || string.IsNullOrWhiteSpace(Notes) || Notes == lastLocalizedNotes)
        {
            Notes = localizedNotes;
        }

        lastLocalizedPresenterName = localizedPresenterName;
        lastLocalizedNotes = localizedNotes;
    }

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        ApplyLocalizedSeeds();
        OnPropertyChanged(nameof(Summary));
    }
}
