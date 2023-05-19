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
            // var scrape_test = new AnimePaheScraper();
            // scrape_test.EpisodeStreamLinks("1d97fa54-3740-5648-69aa-7d39575bfe1c","b2e3ab2f92541b15c7d8c37be07c66526ae0ddf9b82737205d60302a1d0bff5c");
     
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