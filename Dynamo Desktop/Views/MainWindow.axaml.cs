using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;
using Dynamo_Desktop.Scrapers.Anime;
using Dynamo_Desktop.Services;
using Dynamo_Desktop.ViewModels;
using Dynamo_Desktop.Views;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
namespace Dynamo_Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
			InitializeComponent();
            ContentFrame.Navigate(typeof(Anime.Index));
			DataContext = new MainWindowViewModel();
         
            // AppTheme.SetTheme("Dark");

 
        }
        public void BackRequested(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
        public void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            ContentFrame.Tag = e.Parameter;
        }
        public void NavigationItemChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
          
        }
        
    }
}