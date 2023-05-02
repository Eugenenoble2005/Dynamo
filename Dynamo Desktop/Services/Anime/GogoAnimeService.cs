using Dynamo_Desktop.Models.Anime;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime;

public class GogoAnimeService
{
    private static HttpClient client = new HttpClient();
    public static async Task<GogoAnimeRecentEpisodes> RecentEpisodes(int Page=1,int Type=1)
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
            return null;
        }
        catch {
            return null;
        } 
    }
    public static async Task<GogoAnimePopularAnime> PopularEpisodes(int Page=1)
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
            return null;
        }
        catch
        {
            return null;
        }
    }
}
