using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Dynamo_Desktop.Scrapers.Anime;
using Dynamo_Desktop.Services;
using Dynamo_Desktop.ViewModels;
using Dynamo_Desktop.Views;
using Dynamo_Desktop.Views.Hentai;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Navigation;
using FluentAvalonia.UI.Windowing;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using LibVLCSharp.Shared;
using Dynamo_Desktop.Views.SplashScreen;

namespace Dynamo_Desktop.Views
{
    public partial class MainWindow : AppWindow
    {
        public bool IsWindows = true;
        public MainWindow()
        {
           
			InitializeComponent();
            ContentFrame.Navigate(typeof(Anime.Index));
           // SplashScreen = new ApplicationSplashScreen();
			DataContext = new MainWindowViewModel();
            nvSample.AttachedToVisualTree += (sender,args) =>
            {
                nvSample.SelectedItem = nvSample.MenuItems.ElementAt(0);
            };
            TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
            TitleBar.ExtendsContentIntoTitleBar = true;

            TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(50, 255, 255, 255);
            TitleBar.ButtonInactiveForegroundColor = Color.Parse("white");

            nvSample.PropertyChanged += (sender, args) =>
            {
                if(args.Property.ToString() == "SelectedItem")
                {
                    switch(args.NewValue.ToString()){
                        case "Hentai":
                            ContentFrame.Navigate(typeof(Hentai.Index));
                            break;
                        case "Anime":
                            ContentFrame.Navigate(typeof(Anime.Index));
                            break;
                    }
                }
            };
            // AppTheme.SetTheme("Dark");

 
        }
        public void BackRequested(object sender, NavigationViewBackRequestedEventArgs e)
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
    public class ApplicationSplashScreen : IApplicationSplashScreen
    {
        public Task RunTasks(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //try load vlc now to prevent slow loads later on;
                try
                {
                    LibVLC _libVlc = new LibVLC(enableDebugLogs: true);
                }
                catch
                {
                    //exception may be thrown on linux if vlc is not installed. Prompy user to run:
                    //sudo apt install vlc
                }
            });
        }
        public string AppName { get; }
        public IImage AppIcon { get; }
    public object SplashScreenContent => new SplashScreen();
        public int MinimumShowTime => 10000;
    }
