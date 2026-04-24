using System;
using System.Windows;
using System.Windows.Controls;
using GitLabOop.Services;

namespace GitLabOop.Views;

public partial class OneWayBindingsView : UserControl
{
    private string lastLocalizedStorageText = string.Empty;

    public OneWayBindingsView()
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
        var localizedStorageText = LocalizationManager.Instance.GetString("OneWayDirectStorageInitial");
        var currentTag = DirectSourceStorage.Tag as string;
        if (forceUpdate || string.IsNullOrWhiteSpace(currentTag) || currentTag == lastLocalizedStorageText)
        {
            DirectSourceStorage.Tag = localizedStorageText;
        }

        lastLocalizedStorageText = localizedStorageText;
    }
}
