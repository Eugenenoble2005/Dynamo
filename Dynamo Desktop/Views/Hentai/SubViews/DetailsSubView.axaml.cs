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
        DataContext = new DetailsViewModel();
        TagsList.AttachedToVisualTree += (sender, args) =>
        {
            TagsList.Layout = new WrapLayout();
        };
        ServerList.AttachedToVisualTree += (sender, args) =>
        {
          //  ServerList.Layout = new WrapLayout();
        };
    }
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        Frame frame = this.FindAncestorOfType<Frame>();
        if(frame != null)
        {
            HentaiIndexToDetailsRouteParams routeParams = frame.Tag as HentaiIndexToDetailsRouteParams;
            (DataContext as DetailsViewModel).RouteParams = routeParams;
        }
    }
    public void PlayVideo(object sender, RoutedEventArgs e)
    {
         string link = (sender as Button).Tag.ToString();
        // string Title = (DataContext as DetailsViewModel).HentaiDetails.Title;
        // Video.Video videoWindow = new Video.Video(link, Title, Referrer:null,Subtitle:null);
        // videoWindow.ShowDialog(this.FindAncestorOfType<Window>());
        // AcrylicPanel acrylicPanel = this.FindAncestorOfType<AcrylicPanel>();
        // acrylicPanel.ToggleBlur();
        // videoWindow.Closing += (sender, args) =>
        // {
        //     acrylicPanel.ToggleBlur();
        // };
        //attempt mpv
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"mpv",
                Arguments = $"{link}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
               // WorkingDirectory = "/mpv"
            }
        };
        proc.Start();
    }
}
