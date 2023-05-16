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
            scrape_test.EpisodeStreamLinks("aedffcb4-3585-7d9a-0253-ff3870af5f87","748d1000565f23e7dec63a618564777fd19d2f62221cea53616c14a9df79e855");
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