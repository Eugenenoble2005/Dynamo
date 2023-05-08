using Dynamo_Desktop.Scrapers.Anime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Anime
{
    public class AnimePaheService : IAnimeService
    {
        private static HttpClient client = new HttpClient();
        private static AnimePaheScraper _animePaheScraper = new AnimePaheScraper();

        public async Task<T?> Info<T>(string Id = "")
        {
            string json = await _animePaheScraper.AnimeInfo(Id: Id);
            return JsonSerializer.Deserialize<T>(json);
        }

        public Task<T?> PopularEpisodes<T>(int Page)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> RecentEpisodes<T>(int Page=1, int Type=1)
        {
            string recent_releases_endpoint = $"https://animepahe.com/api?m=airing&page={Page}";
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
            catch
            {
                return default;
            }
        }

        public async Task<T?> Search<T>(string Query, int Page)
        {
            string endpoint = $"https://animepahe.com/api?m=search&q={Query}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
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
    }
}
