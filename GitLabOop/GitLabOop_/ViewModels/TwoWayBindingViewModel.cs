using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class TwoWayBindingViewModel : LocalizedViewModelBase
{
    public TwoWayBindingViewModel(ILocalizationService localization)
        : base(localization)
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string fullName = "Иван Иванов";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private string feedback = "Запрос подтвержден";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private double budget = 35000;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LiveSummary))]
    private bool pushEnabled = true;

    public string LiveSummary =>
        string.Format(
            T("TwoWay_LiveSummary"),
            FullName,
            Budget,
            T(PushEnabled ? "Common_Enabled" : "Common_Disabled"),
            Feedback);

    protected override void OnLanguageChanged()
    {
        OnPropertyChanged(nameof(LiveSummary));
    }
}
