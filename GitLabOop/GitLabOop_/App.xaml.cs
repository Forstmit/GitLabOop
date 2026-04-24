using System.Windows;
using GitLabOop.Services;

namespace GitLabOop;

public partial class App : Application
{
    public App()
    {
        LocalizationManager.Instance.SetCulture("ru");
    }
}
