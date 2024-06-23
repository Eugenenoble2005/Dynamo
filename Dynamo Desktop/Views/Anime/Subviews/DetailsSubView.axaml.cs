using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services;
using Dynamo_Desktop.ViewModels.Anime;
using FluentAvalonia.UI.Controls;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dynamo_Desktop.Views.Anime.Subviews
{
    public partial class DetailsSubView : UserControl
    {
        public DetailsSubView()
        {
            InitializeComponent();
            DataContext = new DetailsViewModel();
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            Frame frame = this.FindAncestorOfType<Frame>();
            if (frame != null)
            {
                AnimeIndexToDetailsRouteParams routeParams = frame.Tag as AnimeIndexToDetailsRouteParams;
                (DataContext as DetailsViewModel).RouteParams = routeParams;
            }
           
        }
        private void PresentVideo(object sender, TappedEventArgs e)
        {
            var border = sender as Border;
            string source  = border.Tag.ToString();
            VideoService videoService = new();
            videoService.Play(source);
            videoService.ProcessExited += (o, args) =>
            {
                Debug.WriteLine("Video was exited");
            };
        }

        private void ChangeEpisode(object sender, TappedEventArgs e)
        {
            var border = sender as Border;
            var dataContext = (DataContext as DetailsViewModel);
            var innerText = border.Child as TextBlock; 
            AnimeIndexToDetailsRouteParams routeParams = new()
            {
                Provider = dataContext.RouteParams.Provider,
                AnimeId = dataContext.RouteParams.AnimeId,
                EpisodeNumber = int.Parse(innerText.Text),
                
            };
            dataContext.RouteParams = routeParams;
        }
    }   
   
}
