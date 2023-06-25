using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Dynamo_Desktop.Views.SplashScreen;

public partial class SplashScreen : UserControl
{
    public SplashScreen()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}