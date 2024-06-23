using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels;
using System.Linq;
using Dynamo_Desktop.Services;

namespace Dynamo_Desktop.Views;

public partial class Settings : UserControl
{
    public Settings()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    private void SelectingItemsControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var combo_box = sender as ComboBox;
        string media_player = combo_box.SelectedItem.ToString();
        var current_settings = SettingsService.GetSettings();
        current_settings.media_player = media_player;
        SettingsService.SetSettings(current_settings);
    }
}