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
    public Video(string? Url,string? Title)
    {
        InitializeComponent();
        DataContext = new VideoViewModel();
        (DataContext as VideoViewModel).Url = Url;
        Debug.WriteLine(Url);
        (DataContext as VideoViewModel).Title = Title;
        Opened += (sender, args) =>
        {
            VideoViewer.MediaPlayer = (DataContext as VideoViewModel).MediaPlayer;
            //mount native video to control
            VideoViewer.MediaPlayer.SetHandle(VideoViewer.hndl);
            //autoplay on window open
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