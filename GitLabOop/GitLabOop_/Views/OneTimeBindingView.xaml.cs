using System;
using System.Windows;
using System.Windows.Controls;
using GitLabOop.Services;

namespace GitLabOop.Views;

public partial class OneTimeBindingView : UserControl
{
    private string lastLocalizedSourceText = string.Empty;

    public OneTimeBindingView()
    {
        InitializeComponent();
        ApplyLocalizedValues(forceUpdate: true);
        Loaded += HandleLoaded;
        Unloaded += HandleUnloaded;
    }

    private void HandleLoaded(object sender, RoutedEventArgs e)
    {
        LocalizationManager.Instance.LanguageChanged += HandleLanguageChanged;
    }

    private void HandleUnloaded(object sender, RoutedEventArgs e)
    {
        LocalizationManager.Instance.LanguageChanged -= HandleLanguageChanged;
    }

    private void HandleLanguageChanged(object? sender, EventArgs e)
    {
        ApplyLocalizedValues();
    }

    private void ApplyLocalizedValues(bool forceUpdate = false)
    {
        var localizedSourceText = LocalizationManager.Instance.GetString("OneTimeDirectTextInitial");
        if (forceUpdate || string.IsNullOrWhiteSpace(DirectOneTimeText.Text) || DirectOneTimeText.Text == lastLocalizedSourceText)
        {
            DirectOneTimeText.Text = localizedSourceText;
        }

        lastLocalizedSourceText = localizedSourceText;
    }
}
