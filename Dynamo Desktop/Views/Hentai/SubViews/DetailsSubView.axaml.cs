using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.ViewModels.Hentai;
using FluentAvalonia.UI.Controls;

namespace Dynamo_Desktop.Views.Hentai.SubViews;

public partial class DetailsSubView : UserControl
{
    public DetailsSubView()
    {
        InitializeComponent();
        TagsList.AttachedToVisualTree += (sender, args) =>
        {
            TagsList.Layout = new WrapLayout();
        };

    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        Frame frame = this.FindAncestorOfType<Frame>();
        if(frame != null)
        {
            HentaiIndexToDetailsRouteParams routeParams = frame.Tag as HentaiIndexToDetailsRouteParams;
            DataContext = new DetailsViewModel {RouteParams = routeParams};
        }
    }
    public void PlayVideo(object sender, RoutedEventArgs e)
    {
        // string link = (sender as Button).Tag.ToString();
        // string Title = (this.FindAncestorOfType<Details>().DataContext as DetailsViewModel).GogoAnimeInfo.title;
        // Video.Video videoWindow = new Video.Video(link, Title, Referrer:null,Subtitle:null);
        // videoWindow.Show();

    }
}
