using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels.Hentai;

namespace Dynamo_Desktop.Views.Hentai.SubViews;

public partial class IndexSubView : UserControl
{
    public HentaiProviders Provider { get; set; }
    public IndexSubView()
    {
        InitializeComponent();
        DataContext = new IndexViewModel();

    }
    public void NavigateToDetails(object sender, RoutedEventArgs e)
    {
     
    }
}