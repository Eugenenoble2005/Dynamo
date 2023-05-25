using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.ViewModels.Anime;
using Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
namespace Dynamo_Desktop.Views.Anime.Subviews.Gogo;

public partial class DetailsSubView : UserControl
{
    public DetailsSubView()
    {
        InitializeComponent();
    }
    public void PlayVideo(object sender, RoutedEventArgs e)
    {
        string link = (sender as Button).Tag.ToString();
        string Title = (this.FindAncestorOfType<Details>().DataContext as DetailsViewModel).GogoAnimeInfo.title;
        Video.Video videoWindow = new Video.Video(link, Title, "https://kwik.cx");
        videoWindow.Show();

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
    public void EpisodesListAttached(object sender, VisualTreeAttachmentEventArgs e)
    {
        //apply wraplayout dynamically. This is to avoid runtime problems and cache tha arise from setting this before the content is ready.
        (sender as ItemsRepeater).Layout = new WrapLayout();


    }
}