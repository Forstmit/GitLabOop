using System.Windows;
using GitLabOop.Services;
using GitLabOop.ViewModels;

namespace GitLabOop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var localization = (ILocalizationService)Application.Current.Resources["Loc"];
        DataContext = new MainViewModel(localization);
    }
}
