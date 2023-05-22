using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;
using Avalonia.Interactivity;
namespace Dynamo_Desktop.Video;

public partial class Video : Window
{
    public Video(string? Url)
    {
        InitializeComponent();
        DataContext = new VideoViewModel();
        (DataContext as VideoViewModel).Url = Url;
        Opened += (sender, args) =>
        {
            VideoViewer.MediaPlayer = (DataContext as VideoViewModel).MediaPlayer;
            VideoViewer.MediaPlayer.SetHandle(VideoViewer.hndl);
            (DataContext as VideoViewModel).Play();
        };
     
    }
    public void StopAllPlayers()
    {
        (DataContext as VideoViewModel).StopPlayer();
    }
  public void ToggleFullscreen(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.FullScreen)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.FullScreen;
        }
   
    }
 
 
    public Video()
    {
        InitializeComponent();  
    }
}