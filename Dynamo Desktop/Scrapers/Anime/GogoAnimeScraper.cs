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
using HtmlAgilityPack;

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
                            AnimeId = string.Join("-",item.url.Split("/").Last().Split("-")[..^2]),
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
                            AnimeId =  string.Join("-",item.episodeURL.Split("/").Last().Split("-")[..^2]),
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
        string url = $"{Host}/category/{Query}";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetStringAsync(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            HtmlNode anime_info_body = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='anime_info_body']");
            string anime_poster = anime_info_body.SelectSingleNode(".//img").GetAttributeValue("src", "");
            string anime_title = anime_info_body.SelectSingleNode(".//h1").InnerText;
            string description = anime_info_body.SelectSingleNode(".//div[@class='description']").InnerText;


            using (var httpCLient2 = new HttpClient())
            {
                //should work for 99.999 percent of all cases
                GogoAnimeInfoJsonResponse infoResponse = JsonSerializer.Deserialize<GogoAnimeInfoJsonResponse>(await httpCLient2.GetStringAsync($"https://v2.gogoanimehome.com/anime/details?video={Query}-episode-1")) ;0
                Info.EpisodeCount = infoResponse.data.animeData.episodeNumber.Length;
                Info.Title = anime_title;
                Info.Description = description;
                Info.Image = anime_poster;
            }
            
        }
        return JsonSerializer.Serialize(Info);
    }
}
