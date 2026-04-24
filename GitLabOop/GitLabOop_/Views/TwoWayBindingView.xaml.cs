using System;
using System.Windows;
using System.Windows.Controls;
using GitLabOop.Services;

namespace GitLabOop.Views;

public partial class TwoWayBindingView : UserControl
{
    private string lastLocalizedSourceText = string.Empty;

    public TwoWayBindingView()
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
        var localizedSourceText = LocalizationManager.Instance.GetString("TwoWayDirectSourceTextInitial");
        if (forceUpdate || string.IsNullOrWhiteSpace(DirectTwoWaySourceText.Text) || DirectTwoWaySourceText.Text == lastLocalizedSourceText)
        {
            DirectTwoWaySourceText.Text = localizedSourceText;
        }

        lastLocalizedSourceText = localizedSourceText;
    }
}
