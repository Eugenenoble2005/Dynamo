using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Dynamo_Desktop.Scrapers.Anime;
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
            var scrape_test = new KayoAnimeScraper();
            scrape_test.RecentEpisodes();

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