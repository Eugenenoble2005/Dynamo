using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;
using FluentAvalonia.UI.Controls;
using System.Linq;

namespace Dynamo_Desktop.Views.Anime.Subviews.Gogo;

public partial class IndexSubView : UserControl
{
    public IndexSubView()
    {
        InitializeComponent();
    }
    public void NavigateToDetails(object sender, TappedEventArgs e)
    {
        var grid = sender as Grid;
        ContentControl episode = grid.Children.OfType<ContentControl>().FirstOrDefault();
        ContentControl episode_session = grid.Children.OfType<ContentControl>().ToList()[1];
        var navParams = new AnimeIndexToDetailsRouteParams { Provider = "GogoAnime", AnimeId = grid.Tag as string, EpisodeNumber = episode.Content.ToString(), EpisodeId = episode_session.Content.ToString() };
        Frame frame = this.FindAncestorOfType<Frame>();
        frame.Navigate(typeof(Details), navParams);
    }
}