 using Dynamo_Desktop.Models.Anime;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dynamo_Desktop.Services.Anime;

public class GogoAnimeService
{
    private static HttpClient client = new HttpClient();
    public async Task<GogoAnimeStreamingLinks> StreamingLinks(string AnimeId,string EpisodeId)
    {
        string streaming_links = $"https://api.consumet.org/anime/gogoanime/watch/{EpisodeId}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(streaming_links);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GogoAnimeStreamingLinks>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }
    public  async Task<GogoAnimeRecentEpisodes> RecentEpisodes(int Page=1,int Type=1)
    {
        string recent_releases_endpoint = $"https://api.consumet.org/anime/gogoanime/recent-episodes?page={Page}&type={Type}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(recent_releases_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GogoAnimeRecentEpisodes>(json);
            }
            return default;
        }
        catch {
            return default;
        } 
    }
    public  async Task<GogoAnimePopularAnime> PopularEpisodes(int Page=1)
    {
        string popular_anime_endpoint = $"https://api.consumet.org/anime/gogoanime/top-airing?page={Page}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(popular_anime_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GogoAnimePopularAnime>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }
    public  async Task<GogoAnimeSearch> Search(string Query="",int Page=1)
    {
        string search_endpoint = $"https://api.consumet.org/anime/gogoanime/{Query}?page={Page}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(search_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GogoAnimeSearch>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }

    public async Task<GogoAnimeInfo> Info(string Id = "")
    {
        string url = $"https://api.consumet.org/anime/gogoanime/info/{Id}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GogoAnimeInfo>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }

    public Task<T> AllEpisodes<T>(string Id = "",int Page=1)
    {
        throw new System.NotImplementedException();
    }
}
