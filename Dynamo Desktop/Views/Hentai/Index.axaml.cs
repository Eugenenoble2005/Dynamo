using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels.Hentai;
using FluentAvalonia.UI.Controls;

namespace Dynamo_Desktop.Views.Hentai;

public partial class Index : UserControl
{
    public Index()
    {
        InitializeComponent();
        DataContext = new IndexViewModel();
    }
  
}