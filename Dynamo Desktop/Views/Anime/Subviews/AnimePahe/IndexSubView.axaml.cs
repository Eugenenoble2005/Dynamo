using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using FluentAvalonia.UI.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
namespace Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;

public partial class IndexSubView : UserControl
{
    public IndexSubView()
    {
        InitializeComponent();
    }
    public void NavigateToDetails(object sender, RoutedEventArgs e)
    {
        var grid = sender as Grid;
        ContentControl episode = grid.Children.OfType<ContentControl>().FirstOrDefault();
        List<object> anime_details = new List<object> { grid.Tag, episode.Content };
        var navParams = new  AnimeIndexToDetailsRouteParams {Provider = "AnimePahe",AnimeId=grid.Tag as string,EpisodeNumber=episode.Content as string };
        Frame frame = this.FindAncestorOfType<Frame>();
        frame.Navigate(typeof(Details),navParams);
    }


}
public class AnimeIndexToDetailsRouteParams
{
    public string? Provider { get; set; }
    public string? AnimeId { get; set; }
    public string? EpisodeNumber { get; set; }
    public string? EpisodeId { get; set; }
}