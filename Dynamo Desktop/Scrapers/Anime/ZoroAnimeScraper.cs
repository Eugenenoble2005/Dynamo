using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dynamo_Desktop.Models.Anime;
using Dynamo_Desktop.Services;
using HtmlAgilityPack;

namespace Dynamo_Desktop.Scrapers.Anime;


public class ZoroAnimeScraper
{
    private string Host => SettingsService.Settings().Providers.zoroanime.host;

    public async Task<string> PopularOrRecent(int Page = 1, string Query = "Recent")
    {
        var handler = new HttpClientHandler();
        handler.UseCookies = false;
        handler.AutomaticDecompression = ~DecompressionMethods.None; 
        string url =  Query == "Recent" ?  $"{Host}/newest?page={Page}" : $"{Host}/ongoing?page={Page}";
        using (var httpClient = new HttpClient(handler))
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:126.0) Gecko/20100101 Firefox/126.0");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                request.Headers.TryAddWithoutValidation("Referer", $"{Host}/home");
                request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "document");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "navigate");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-User", "?1");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request.Headers.TryAddWithoutValidation("Alt-Used", Host);
                request.Headers.TryAddWithoutValidation("Priority", "u=1");
                request.Headers.TryAddWithoutValidation("TE", "trailers");
                var response = await httpClient.SendAsync(request);
                HtmlDocument htmlDoc = new HtmlDocument();
                Debug.WriteLine(response.IsSuccessStatusCode);
                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }
                htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync());
                List<PopularAnime> zoro_recent_episodes = new();
                HtmlNode main_content = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='main-content']");
                HtmlNodeCollection flw_items = main_content.SelectNodes(".//div[contains(@class,'flw-item')]");
                foreach(HtmlNode flw_item in flw_items)
                {
                    PopularAnime anime = new();
                    anime.Title = flw_item.SelectSingleNode(".//a[@class='d-title']").InnerText;
                    anime.Image = flw_item.SelectSingleNode(".//img").GetAttributeValue("data-src", null);
                    anime.AnimeId = flw_item.SelectSingleNode(".//a[@class='d-title']").GetAttributeValue("href",null).TrimStart('/');
                    anime.Episode = 1;
                    zoro_recent_episodes.Add(anime);
                }
                return JsonSerializer.Serialize(zoro_recent_episodes);
            }
        }

        return default;
    }
}