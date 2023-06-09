using Avalonia;
using FluentAvalonia.Styling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services;

public class AppTheme
{
    public static void SetTheme(string requested_theme)
    {
        if (requested_theme == "Light")
        {
            //updates avalonia to v11. This is no longer needed.
            //Application.Current.Styles.OfType<FluentTheme>().FirstOrDefault().Mode = FluentThemeMode.Light;
            //Application.Current.Styles.OfType<FluentAvaloniaTheme>().FirstOrDefault().RequestedTheme = FluentAvaloniaTheme.LightModeString;
            //Debug.WriteLine(Application.Current.Resources["MicaBackground"]);
            //Application.Current.Resources["MicaBackground"] = Avalonia.Media.Color.Parse("white");
        }
        else
        {
            //Application.Current.Styles.OfType<FluentTheme>().FirstOrDefault().Mode = FluentThemeMode.Dark;
            //Application.Current.Styles.OfType<FluentAvaloniaTheme>().FirstOrDefault().RequestedTheme = FluentAvaloniaTheme.DarkModeString;
            //Application.Current.Resources["MicaBackground"] = Avalonia.Media.Color.Parse("black");
        }
    }
}
