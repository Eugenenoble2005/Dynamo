using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.ViewModels.Anime;
using FluentAvalonia.UI.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Dynamo_Desktop.Views.Anime.Subviews
{
    public partial class IndexSubView : UserControl
    {
        public AnimeProviders Provider { get; set; }
        public IndexSubView()
        {
            InitializeComponent();
           
        }
        public void NavigateToDetails(object sender, TappedEventArgs e)
        {
            Grid subject = sender as Grid;
            //content control here is just used to hold data for routing
            List<ContentControl> content_children = subject.Children.OfType<ContentControl>().ToList();
            AnimeIndexToDetailsRouteParams routeParams = new()
            {
                AnimeId = content_children[0].Content.ToString(),
                EpisodeNumber = int.Parse(content_children[1].Content.ToString()),
                Provider = (DataContext as IndexViewModel).Provider
            };
            this.FindAncestorOfType<Frame>().Navigate(typeof(DetailsSubView),routeParams);
        }
    }
}
