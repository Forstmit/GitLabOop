namespace GitLabOop.ViewModels;

public class MainViewModel
{
    public DefaultBindingViewModel DefaultBinding { get; } = new();

    public TwoWayBindingViewModel TwoWayBinding { get; } = new();

    public OneTimeBindingViewModel OneTimeBinding { get; } = new();

    public OneWayBindingsViewModel OneWayBindings { get; } = new();

    public TriggersViewModel Triggers { get; } = new();
}
