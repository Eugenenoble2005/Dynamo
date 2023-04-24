using Avalonia.Controls;
using Avalonia.Themes.Fluent;
using Dynamo_Desktop.Services;
using Dynamo_Desktop.ViewModels;
using Dynamo_Desktop.Views;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;
using System.Linq;

namespace Dynamo_Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(typeof(Anime.Index));
            DataContext = new MainWindowViewModel();
            AppTheme.SetTheme("Dark");
        }
        public void NavigationSelectionChanged()
        { 
            Debug.WriteLine("Navigation Chnaged");
        }
    }
}