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

    public async Task<string> PopularOrRecent(int Page = 1, string Query = "Popular")
    {
        List<PopularAnime> PopularAnime = new();
        string url = Query == "Popular" ? $"https://v2.gogoanimehome.com/anime/home?page={Page}&type=popular" : $"https://v2.gogoanimehome.com/anime/home?page={Page}";
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
            {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = JsonSerializer.Deserialize<GogoAnimePopularJsonResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var item in responseBody.data.animeData)
                    {
                        PopularAnime.Add(new()
                        {
                            AnimeId = string.Join("-", item.url.Split("/").Last().Split("-")[..^2]),
                            Title = item.name,
                            Image = item.img,
                            Episode = int.Parse(item.url.Split("-").Last())
                        });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(PopularAnime);
    }

    public async Task<string> Search(string Query, int Page = 1)
    {
        List<PopularAnime> SearchResults = new();
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://v2.gogoanimehome.com/anime/search?keyword={Query}&page={Page}"))
            {
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<GogoAnimeSearchJsonResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var item in responseData.data.animeData)
                    {
                        SearchResults.Add(new()
                        {
                            AnimeId = string.Join("-", item.episodeURL.Split("/").Last().Split("-")[..^2]),
                            Image = item.episodeImage,
                            Title = item.episodeName
                        }) ;
                    }
                }
            }
        }
        return JsonSerializer.Serialize(SearchResults);
    }
    public async Task<string> Info(string Query)
    {
        AnimeInfo Info = new();
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://v2.gogoanimehome.com/anime/details?video={Query}"))
            {
                var response = await httpClient.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var responseBody = JsonSerializer.Deserialize<GogoAnimeInfoJsonResponse>(await response.Content.ReadAsStringAsync());
                    Info.Title = responseBody.data.animeData.episodeName;
                    Info.Description = null;
                    Info.EpisodeCount = responseBody.data.animeData.episodeNumber.Length; 
                }
            }
        }
        return JsonSerializer.Serialize(Info);
    }
}
