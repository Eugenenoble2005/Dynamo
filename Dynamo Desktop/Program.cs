using Avalonia;
using Avalonia.ReactiveUI;
using CefNet;
using Dynamo_Desktop.Scrapers.Anime;
using EmbedIO;
using System;
using System.Diagnostics;
using System.IO;

namespace Dynamo_Desktop
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public  static void Main(string[] args)
        {
            // var scrape_test = new AnimePaheScraper();
            // scrape_test.EpisodeStreamLinks("1d97fa54-3740-5648-69aa-7d39575bfe1c","b2e3ab2f92541b15c7d8c37be07c66526ae0ddf9b82737205d60302a1d0bff5c");
            var webRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "webcomponents");
            var server = new WebServer("http://localhost:2005");
            server.WithStaticFolder("/", webRootPath, false);
            server.RunAsync();
            var settings = new CefSettings();
            settings.NoSandbox = true;
            settings.MultiThreadedMessageLoop = false; // or true
            settings.WindowlessRenderingEnabled = true;
            settings.LocalesDirPath = "C:\\Users\\noble\\Downloads\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\Resources\\locales";
            settings.ResourcesDirPath = "C:\\Users\\noble\\Downloads\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\Resources";

            var app = new CefNetApplication();
            app.Initialize("C:\\Users\\noble\\Downloads\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\cef_binary_105.3.39+g2ec21f9+chromium-105.0.5195.127_windows64\\Release", settings);
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
           
        }


        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}