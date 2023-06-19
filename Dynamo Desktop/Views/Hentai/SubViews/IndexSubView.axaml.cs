using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.ViewModels.Hentai;
using FluentAvalonia.UI.Controls;

namespace Dynamo_Desktop.Views.Hentai.SubViews;

public partial class IndexSubView : UserControl
{
    public HentaiProviders Provider { get; set; }
    public IndexSubView()
    {
        InitializeComponent();
    }
    public void NavigateToDetails(object sender, TappedEventArgs e)
    {
        Grid grid = (sender as Grid);
        string hentaiId = grid.Tag.ToString();
        var routeParams = new HentaiIndexToDetailsRouteParams { HentaiId = hentaiId,Provider = Provider};
        Frame frame = this.FindAncestorOfType<Frame>();
        frame.Navigate(typeof(DetailsSubView),routeParams);
    }
}

public class HentaiIndexToDetailsRouteParams
{
    public string HentaiId { get; set; }
    public HentaiProviders Provider { get; set; }
}