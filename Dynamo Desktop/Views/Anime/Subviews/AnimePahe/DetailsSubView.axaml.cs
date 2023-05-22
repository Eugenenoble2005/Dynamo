using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.ViewModels.Anime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        string Title = (this.FindAncestorOfType<Details>().DataContext as DetailsViewModel).AnimePaheAnimeInfo.Title;
        Video.Video videoWindow = new Video.Video(link,Title);
        videoWindow.Show();
        videoWindow.Closing += (s, e) =>
        {
            videoWindow.StopAllPlayers();
        };
    }
    public void ChangeEpisode(object sender, RoutedEventArgs e)
    {
        Button button = (sender as Button);
        List<string> parameters = button.Tag.ToString().Split("|").ToList();
        Details DetailsParent = this.FindAncestorOfType<Details>();
        (DataContext as DetailsViewModel).RouteParams = new AnimeIndexToDetailsRouteParams
        {

            Provider = (DataContext as DetailsViewModel).RouteParams.Provider,
            EpisodeNumber = parameters[0],
            EpisodeId = parameters[1],
            AnimeId = (DataContext as DetailsViewModel).RouteParams.AnimeId
        };
        //(DataContext as DetailsViewModel).GetAnimeDetails();
    }
}