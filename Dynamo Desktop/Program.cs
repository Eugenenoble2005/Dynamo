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
            //var scrape_test = new AnimePaheScraper();
            // scrape_test.AnimeInfo("d406c40f-294b-e1eb-0b2e-ea13cea6fc08");
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