using CommunityToolkit.Mvvm.ComponentModel;
using GitLabOop.Services;

namespace GitLabOop.ViewModels;

public partial class DefaultBindingViewModel : LocalizedViewModelBase
{
    public DefaultBindingViewModel(ILocalizationService localization)
        : base(localization)
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private string presenterName = "Утренний бриф команды";

    [ObservableProperty]
    private string notes = "Изменения в TextBox и CheckBox уходят в модель без явного указания режима.";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AccentOpacityRatio))]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private double accentOpacity = 68;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Summary))]
    private bool isPinned = true;

    public double AccentOpacityRatio => AccentOpacity / 100.0;

    public string Summary =>
        string.Format(T("Default_Summary"), PresenterName, AccentOpacity, T(IsPinned ? "Common_Yes" : "Common_No"));

    protected override void OnLanguageChanged()
    {
        OnPropertyChanged(nameof(Summary));
    }
}
