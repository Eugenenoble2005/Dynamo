using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Dynamo_Desktop.Models.Anime;
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
            DataContext = new DetailsViewModel2();
            InitializeComponent();
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            Frame frame = this.FindAncestorOfType<Frame>();
            if (frame != null)
            {
                AnimeIndexToDetailsRouteParams routeParams = frame.Tag as AnimeIndexToDetailsRouteParams;
                (DataContext as DetailsViewModel2).RouteParams = routeParams;
            }
            base.OnAttachedToVisualTree(e);
        }
    }   
   
}
