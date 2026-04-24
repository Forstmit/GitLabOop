using CommunityToolkit.Mvvm.ComponentModel;

namespace GitLabOop.ViewModels;

public partial class DefaultBindingViewModel : ObservableObject
{
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

    public string Summary => $"{PresenterName} | Прозрачность акцента: {AccentOpacity:F0}% | Закреплено: {(IsPinned ? "Да" : "Нет")}";
}
