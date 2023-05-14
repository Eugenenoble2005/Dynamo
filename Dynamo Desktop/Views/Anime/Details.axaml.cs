using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent.Controls;
using Avalonia.VisualTree;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;
using Dynamo_Desktop.ViewModels.Anime;
using System.Text.Json;
using Dynamo_Desktop.Views.Anime.Subviews.AnimePahe;

namespace Dynamo_Desktop.Views.Anime;

public partial class Details : UserControl
{
    public Details()
    {
        InitializeComponent();
        DataContext = new DetailsViewModel();
    }
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        Frame frame = this.FindAncestorOfType<Frame>();
        if(frame != null)
        {
            var routeParams = frame.Tag as AnimeIndexToDetailsRouteParams;
            (DataContext as DetailsViewModel).RouteParams = routeParams;
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        //purge data context on exit to prevent unnecessary caching
         DataContext = new DetailsViewModel();
    }
}   