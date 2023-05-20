using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.Text.Json;
namespace Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;

public partial class DetailsSubView : UserControl
{
    public DetailsSubView()
    {
        InitializeComponent();
    }
    public void PlayVideo(object sender, RoutedEventArgs e)
    {
        string link = (sender as Button).Tag.ToString();
        Video.Video videoWindow = new Video.Video(link);
        videoWindow.Show();
        videoWindow.Closing += (s, e) =>
        {
            videoWindow.StopAllPlayers();
        };
    }
}