using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitLabOop.Models;

namespace GitLabOop.ViewModels;

public partial class OneTimeBindingViewModel : ObservableObject
{
    [ObservableProperty]
    private string draftTitle = "Прототип отчёта";

    [ObservableProperty]
    private string draftComment = "Снимок фиксирует состояние только в момент создания.";

    [ObservableProperty]
    private double draftProgress = 38;

    [ObservableProperty]
    private BindingSampleModel snapshot;

    public OneTimeBindingViewModel()
    {
        snapshot = CreateSnapshot();
    }

    [RelayCommand]
    private void RefreshSnapshot()
    {
        Snapshot = CreateSnapshot();
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
}
