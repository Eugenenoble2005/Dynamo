using Dynamo_Desktop.Models.Anime;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dynamo_Desktop.Services.Anime;

public class GogoAnimeService : IAnimeService
{
    private static HttpClient client = new HttpClient();

    public  async Task<T?> RecentEpisodes<T>(int Page=1,int Type=1)
    {
        string recent_releases_endpoint = $"https://api.consumet.org/anime/gogoanime/recent-episodes?page={Page}&type={Type}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(recent_releases_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }
        catch {
            return default;
        } 
    }
    public  async Task<T?> PopularEpisodes<T>(int Page=1)
    {
        string popular_anime_endpoint = $"https://api.consumet.org/anime/gogoanime/top-airing?page={Page}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(popular_anime_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }
    public  async Task<T?> Search<T>(string Query="",int Page=1)
    {
        string search_endpoint = $"https://api.consumet.org/anime/gogoanime/{Query}?page={Page}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(search_endpoint);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }
        catch
        {
            return default;
        }
    }

    public Task<T?> Info<T>(string Id = "")
    {
        throw new System.NotImplementedException();
    }
}
