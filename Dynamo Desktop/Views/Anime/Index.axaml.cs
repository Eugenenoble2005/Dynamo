using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Dynamo_Desktop.ViewModels.Anime;
using FluentAvalonia.UI.Controls;
using System.ComponentModel;
using System.Diagnostics;

namespace Dynamo_Desktop.Views.Anime;

public partial class Index : UserControl
{
    public Index()
    {
        InitializeComponent();
        DataContext = new IndexViewModel();
    }
    public void SortComboChanged()
    {
        Debug.WriteLine("Test"); 
    }
}

