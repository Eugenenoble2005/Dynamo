using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Dynamo_Desktop.ViewModels.Hentai;
using FluentAvalonia.UI.Controls;
using System.Diagnostics;

namespace Dynamo_Desktop.Views.Hentai;

public partial class Index : UserControl
{
    private bool _hasShownWarning = false;
    public Index()
    {
        InitializeComponent();
        DataContext = new IndexViewModel();
   
    }
    private async void ShowNsfwWarning()
    {
        Debug.WriteLine(_hasShownWarning);
        AcrylicPanel acrylicPanel = this.FindAncestorOfType<AcrylicPanel>();
        if(acrylicPanel != null)
        {
            Debug.WriteLine("Acrylic panel is not null");
            acrylicPanel.ToggleBlur();
            ContentDialog warning = new ContentDialog
            {
                Title = "Warning",
                 Content="The following content is NSFW. Do you want to proceed?",
                 PrimaryButtonText="Yes",
                 SecondaryButtonText="No",
                 DefaultButton=ContentDialogButton.Primary
            };
            ContentDialogResult warning_result = await warning.ShowAsync();
            if(warning_result == ContentDialogResult.Primary)
            {
                acrylicPanel.ToggleBlur();
                _hasShownWarning = true;
            }
            else
            {
                acrylicPanel.ToggleBlur();
                Frame frame = this.FindAncestorOfType<Frame>();
                frame.GoBack();
            }
        }
    }
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (!_hasShownWarning)
        {
            ShowNsfwWarning();
        }
    }
}