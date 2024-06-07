using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels.Anime;
using FluentAvalonia.UI.Controls;
using System.ComponentModel;
using System.Diagnostics;
using Dynamo_Desktop.Views.Anime.Subviews;

namespace Dynamo_Desktop.Views.Anime;

public partial class Index : UserControl
{
    public Index()
    {
        InitializeComponent();
        DataContext = new IndexViewModel();
        Tab.SelectionChanged += (sender, args) =>
        {
            if (DataContext != null)
            {
                var provider = (Tab.SelectedContent as IndexSubView).Provider;
                (DataContext as IndexViewModel).Provider = provider;
                
            }
        };
    }
    public void SortComboChanged()
    {
        Debug.WriteLine("Test"); 
    }

    private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
     
    }
}

