using System.Windows;
using GitLabOop.ViewModels;

namespace GitLabOop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
