using Dynamo_Desktop.Services;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using Dynamo_Desktop.Models.Anime;

namespace Dynamo_Desktop.Scrapers.Anime;
internal partial class GogoAnimeScraper
{
    private string Host => SettingsService.Settings().Providers.gogoanime.host;

    public async Task<string> Popular(int Page = 1)
    {
        List<PopularAnime> PopularAnime = new();
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://v2.gogoanimehome.com/anime/home?page={Page}&type=popular"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:126.0) Gecko/20100101 Firefox/126.0");
                request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br, zstd");
                request.Headers.TryAddWithoutValidation("Origin", $"{Host}");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request.Headers.TryAddWithoutValidation("Referer", $"{Host}");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "cross-site");
                request.Headers.TryAddWithoutValidation("If-None-Match", "W/");
                request.Headers.TryAddWithoutValidation("TE", "trailers");

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = JsonSerializer.Deserialize<GogoAnimePopularJsonRequest>(await response.Content.ReadAsStringAsync());
                    foreach (var item in responseBody.data.animeData)
                    {
                        PopularAnime.Add(new()
                        {
                            AnimeId = string.Join("-",item.url.Split("/").Last().Split("-")[..^2]),
                            Title = item.name,
                            Image = item.img,
                        });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(PopularAnime);
    }
}
