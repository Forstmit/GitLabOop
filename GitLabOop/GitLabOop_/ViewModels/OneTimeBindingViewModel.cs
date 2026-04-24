using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Models;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class OneTimeBindingViewModel : ObservableObject
{
    [ObservableProperty]
    private string draftTitle = string.Empty;

    [ObservableProperty]
    private string draftComment = string.Empty;

    [ObservableProperty]
    private double draftProgress = 38;

    [ObservableProperty]
    private BindingSampleModel snapshot = new();

    [ObservableProperty]
    private string lastActionMessage = string.Empty;

    private string lastLocalizedDraftTitle = string.Empty;
    private string lastLocalizedDraftComment = string.Empty;
    private bool hasSnapshotMessage;

    public OneTimeBindingViewModel()
    {
        ApplyLocalizedSeeds(forceUpdate: true);
        snapshot = CreateSnapshot();
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    [RelayCommand]
    private void RefreshSnapshot()
    {
        Snapshot = CreateSnapshot();
        hasSnapshotMessage = true;
        LastActionMessage = LocalizationManager.Instance.Format("SnapshotUpdatedMessage", Snapshot.NumericValue);
    }

    private BindingSampleModel CreateSnapshot()
    {
        return new BindingSampleModel
        {
            PrimaryText = DraftTitle,
            SecondaryText = DraftComment,
            NumericValue = DraftProgress,
            Flag = DraftProgress >= 50
        };
    }

    private void ApplyLocalizedSeeds(bool forceUpdate = false)
    {
        var localizer = LocalizationManager.Instance;
        var localizedDraftTitle = localizer.GetString("OneTimeDraftTitleInitial");
        var localizedDraftComment = localizer.GetString("OneTimeDraftCommentInitial");

        if (forceUpdate || string.IsNullOrWhiteSpace(DraftTitle) || DraftTitle == lastLocalizedDraftTitle)
        {
            DraftTitle = localizedDraftTitle;
        }

        if (forceUpdate || string.IsNullOrWhiteSpace(DraftComment) || DraftComment == lastLocalizedDraftComment)
        {
            DraftComment = localizedDraftComment;
        }

        lastLocalizedDraftTitle = localizedDraftTitle;
        lastLocalizedDraftComment = localizedDraftComment;
    }

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        ApplyLocalizedSeeds();

        if (hasSnapshotMessage)
        {
            LastActionMessage = LocalizationManager.Instance.Format("SnapshotUpdatedMessage", Snapshot.NumericValue);
        }
    }
}
