using Avalonia;
using Avalonia.ReactiveUI;
using Dynamo_Desktop.Scrapers.Anime;
using System;

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
            var scrape_test = new AnimePaheScraper();
            scrape_test.EpisodeStreamLinks("fed87bb9-2ae7-f5fc-07a5-b8b015d3e1a1","e34c9629142f9adb6c909024b8e1843637d10c9a06cce6f1f277bfc7f39ab811");
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