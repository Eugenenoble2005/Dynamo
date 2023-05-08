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
            //scrape_test.AnimeInfo("1c376b19-ffef-f66f-3345-0174d2775cbc");
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